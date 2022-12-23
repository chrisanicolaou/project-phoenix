using System;
using System.Collections;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Utils;
using ChiciStudios.ProjectPhoenix.VariableSO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI
{
    public class PlayerHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Sprite _fullHeart;

        [SerializeField]
        private Sprite _threeQuarterHeart;

        [SerializeField]
        private Sprite _halfHeart;

        [SerializeField]
        private Sprite _quarterHeart;

        [SerializeField]
        private Sprite _emptyHeart;

        [SerializeField]
        private IntVariable _maxHealth;

        [SerializeField]
        private IntVariable _currentHealth;

        [SerializeField]
        private Color _hurtHeartColor;

        [SerializeField]
        [Range(0.1f, 1)]
        private float _hurtHeartShakeDuration;

        [SerializeField]
        [Range(0.1f, 3)]
        private float _hurtHeartShakeStrength;

        [SerializeField]
        [Range(5, 30)]
        private int _hurtHeartShakeVibrato;
        
        [SerializeField]
        private AudioSource _audioSource;

        [SerializeField]
        private AudioClip _lowHealthSfx;

        [SerializeField]
        [Range(0.1f, 0.3f)]
        private float _sfxThreshold;

        private IEnumerator _lowHealthSfxCoroutine;

        private int _previousCurrentHealth;

        private Transform _transform;

        private Dictionary<int, Sprite> _remainderHeartLookup;

        private List<Image> _heartsInScene = new List<Image>();

        private void Start()
        {
            _transform = transform;
            _remainderHeartLookup = new Dictionary<int, Sprite>
            {
                { 1, _quarterHeart },
                { 2, _halfHeart },
                { 3, _threeQuarterHeart },
            };
            _previousCurrentHealth = _currentHealth.Value;
            CreateHearts();
            _currentHealth.ValueChanged += OnCurrentHealthChange;
            _maxHealth.ValueChanged += CreateHearts;
        }

        private void OnCurrentHealthChange()
        {
            RedrawHearts();

            if (_previousCurrentHealth > _currentHealth.Value)
            {
                PlayLostHealthAnimation();
            }

            if ((float)_currentHealth.Value / _maxHealth.Value <= _sfxThreshold)
            {
                if (_lowHealthSfxCoroutine == null)
                {
                    _lowHealthSfxCoroutine = PlayLowHealthSound();
                    StartCoroutine(_lowHealthSfxCoroutine);
                }
            }
            else
            {
                if (_lowHealthSfxCoroutine != null)
                {
                    StopCoroutine(_lowHealthSfxCoroutine);
                    _lowHealthSfxCoroutine = null;
                }
            }

            _previousCurrentHealth = _currentHealth.Value;
        }

        private void CreateHearts()
        {
            if (_transform.childCount > 0)
            {
                _transform.DestroyAllChildren();
            }

            _heartsInScene = new List<Image>();
            var remainder = _currentHealth.Value % 4;
            var heartsDrawn = 0;
            for (var i = 4; i <= _currentHealth.Value; i += 4)
            {
                DrawHeart(_fullHeart);
                heartsDrawn++;
            }

            if (_currentHealth.Value == _maxHealth.Value) return;

            if (remainder != 0)
            {
                DrawHeart(_remainderHeartLookup[remainder]);
                heartsDrawn++;
            }

            var emptyHeartsToDraw = Mathf.CeilToInt(_maxHealth.Value / 4f) - heartsDrawn;

            for (int i = 0; i < emptyHeartsToDraw; i++)
            {
                DrawHeart(_emptyHeart);
            }
        }

        private void RedrawHearts()
        {
            var remainder = _currentHealth.Value % 4;
            var i = 4;
            var remainderDrawn = false;
            foreach (var heart in _heartsInScene)
            {
                if (i <= _currentHealth.Value)
                {
                    heart.sprite = _fullHeart;
                    i += 4;
                    continue;
                }

                if (remainder != 0 && !remainderDrawn)
                {
                    heart.sprite = _remainderHeartLookup[remainder];
                    remainderDrawn = true;
                    continue;
                }

                heart.sprite = _emptyHeart;
            }
        }

        private void PlayLostHealthAnimation()
        {
            foreach (var heart in _heartsInScene)
            {
                heart.color = _hurtHeartColor;
            }

            var healthDiff = _previousCurrentHealth - _currentHealth.Value;
            var lossFraction = healthDiff / (float)_previousCurrentHealth;
            _transform.DOShakePosition(_hurtHeartShakeDuration, Mathf.Max(lossFraction, 0.2f) * _hurtHeartShakeStrength,
                    vibrato: _hurtHeartShakeVibrato, snapping: false, fadeOut: false)
                .OnComplete(() =>
                {
                    foreach (var heart in _heartsInScene)
                    {
                        heart.color = Color.white;
                    }
                });
        }

        private void DrawHeart(Sprite heart)
        {
            var heartObj = new GameObject("Heart", typeof(RectTransform));
            heartObj.GetComponent<RectTransform>().SetParent(_transform, false);
            var img = heartObj.AddComponent<Image>();
            img.sprite = heart;
            img.SetNativeSize();
            _heartsInScene.Add(img);
        }

        private IEnumerator PlayLowHealthSound()
        {
            while (true)
            {
                _audioSource.PlayOneShot(_lowHealthSfx);
                yield return new WaitForSeconds(1f);
            }
        }

        private void OnDestroy()
        {
            _currentHealth.ValueChanged -= RedrawHearts;
            _maxHealth.ValueChanged -= CreateHearts;
        }
    }
}