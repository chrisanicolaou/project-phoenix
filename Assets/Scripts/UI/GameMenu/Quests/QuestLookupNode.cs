using System;
using ChiciStudios.ProjectPhoenix.Questing;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestLookupNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI _title;

        [SerializeField]
        private Color _selectedCol;
        
        [SerializeField]
        private Color _hoveredCol = Color.black;
        
        [SerializeField]
        [Range(0.01f, 0.3f)]
        private float _hoverTweenDuration = 0.2f;
        
        public Quest Quest { get; set; }

        private bool _hovered;

        private bool _selected;

        private Image _background;

        private Color _backgroundOriginalCol;
        public QuestPreview Preview { get; set; }
        private TweenerCore<Color, Color, ColorOptions> _hoveredTween;

        private void Awake()
        {
            _background = GetComponentInChildren<Image>();
            _backgroundOriginalCol = _background.color;
        }

        public void Deselect()
        {
            _background.color = _backgroundOriginalCol;
            _selected = false;
        }

        public void AssignQuest(Quest quest)
        {
            Quest = quest;
            _title.text = Quest.Name;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovered = true;
            if (_selected) return;
            
            _hoveredTween?.Kill();
            _hoveredTween = _background.DOColor(_hoveredCol, _hoverTweenDuration);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovered = false;
            if (_selected) return;
            
            _hoveredTween?.Kill();
            _hoveredTween = _background.DOColor(_backgroundOriginalCol, _hoverTweenDuration);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_hovered) return;
            Preview.LoadQuest(this);
        }

        public void OnSelect()
        {
            _background.color = _selectedCol;
            _selected = true;
        }
    }
}