using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestTab : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private Sprite _selectedSprite;

        [SerializeField]
        private Sprite _deselectedSprite;

        [SerializeField]
        private QuestTabGroup _tabGroup;

        private Image _img;
        private RectTransform _rect;
        
        private bool _isHovered;
        private bool _isSelected;

        private void Awake()
        {
            _img = GetComponent<Image>();
            _rect = GetComponent<RectTransform>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _isHovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _isHovered = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_isHovered) Select();
        }

        public void Select()
        {
            if (_isSelected) return;
            _img.sprite = _selectedSprite;
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, 1);
            _tabGroup.LoadQuests(this);
            _isSelected = true;
        }

        public void Deselect()
        {
            _img.sprite = _deselectedSprite;
            _rect.anchoredPosition = new Vector2(_rect.anchoredPosition.x, 0);
            _isSelected = false;
        }
    }
}