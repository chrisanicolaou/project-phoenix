using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.VariableSO;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.Player
{
    public class PlayerRespawner : MonoBehaviour, IGameEventConsumer
    {
        [SerializeField]
        private GameEvent _playerDeathEvent;
        
        [SerializeField]
        private CanvasGroup _fadeImg;

        [SerializeField]
        [Range(0.2f, 1f)]
        private float _timeSlowOnDeath;

        [SerializeField]
        private float _fadeOutDelay;

        [SerializeField]
        private float _fadeInDelay;

        [SerializeField]
        private float _fadeDuration;

        [SerializeField]
        private PlayerController _playerController;

        [SerializeField]
        private IntVariable _playerHealth;

        [SerializeField]
        private IntVariable _playerMaxHealth;

        [SerializeField]
        private Vector2Variable _playerSpawnPosition;

        [SerializeField]
        [Range(0.2f, 1f)]
        private float _healthRespawnAmount;

        private void Start()
        {
            // GameEventsManager.Instance.AddListener(GameEvent.PlayerKilled, OnPlayerDeath);
            SubscribeGameEvents();
        }

        private void OnPlayerDeath(Dictionary<string, object> msg)
        {
            Time.timeScale = _timeSlowOnDeath;
            var seq = DOTween.Sequence();
            seq.AppendInterval(_fadeOutDelay);
            seq.Append(_fadeImg.DOFade(1f, _fadeDuration));
            seq.AppendCallback(OnRespawn);
            seq.AppendInterval(_fadeInDelay);
            seq.Append(_fadeImg.DOFade(0f, _fadeDuration));
        }

        private void OnRespawn()
        {
            _playerHealth.Value = Mathf.CeilToInt(_playerMaxHealth.Value * _healthRespawnAmount);
            _playerController.Respawn();
            Time.timeScale = 1f;
        }

        private void OnDestroy()
        {
            UnsubscribeGameEvents();
        }

        public void SubscribeGameEvents()
        {
            _playerDeathEvent.AddListener(this, OnPlayerDeath);
        }

        public void UnsubscribeGameEvents()
        {
            _playerDeathEvent.RemoveListener(this, OnPlayerDeath);
        }
    }
}