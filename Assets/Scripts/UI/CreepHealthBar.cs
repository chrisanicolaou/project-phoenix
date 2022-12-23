using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI
{
    public class CreepHealthBar : MonoBehaviour
    {
        [SerializeField]
        private Slider _fillSlider;
        
        [SerializeField]
        private Slider _backFillSlider;

        [SerializeField]
        [Range(0.1f, 1f)]
        private float _slideDuration;

        [SerializeField]
        private Ease _slideEase;

        public void Init(float maxValue)
        {
            _fillSlider.wholeNumbers = false;
            _fillSlider.maxValue = maxValue;
            _fillSlider.value = maxValue;
            _backFillSlider.wholeNumbers = false;
            _backFillSlider.maxValue = maxValue;
            _backFillSlider.value = maxValue;
        }

        public void Init(int maxValue)
        {
            Init((float)maxValue);
        }

        public void UpdateValue(float value)
        {
            _fillSlider.value = value;
            _backFillSlider.DOValue(value, _slideDuration).SetEase(_slideEase);
        }

        public void UpdateValue(int value)
        {
            var fValue = (float)value;
            UpdateValue(fValue);
        }
    }
}