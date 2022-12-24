using System;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI
{
    public class GameMenuTab : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        [field: SerializeField]
        public Tab TabType { get; set; }

        [SerializeField]
        public GameObject _page;

        [SerializeField]
        public Sprite _selectedSprite;

        [SerializeField]
        public Sprite _deselectedSprite;

        [SerializeField]
        public Sprite _selectedBackgroundSprite;

        [SerializeField]
        public Sprite _deselectedBackgroundSprite;

        [SerializeField]
        private Image _backgroundImg;

        [SerializeField]
        private Image _iconImg;
        
        [SerializeField]
        private GameMenuTabGroup _tabGroup;

        private RectTransform _tabRect;

        [SerializeField]
        private Vector2 _selectedRectSize = new Vector2(19, 30);
        
        [SerializeField]
        private Vector2 _deselectedRectSize = new Vector2(19, 26);

        private void Awake()
        {
            _tabRect = GetComponent<RectTransform>();
        }

        public void Select()
        {
            if (this == _tabGroup.SelectedTab) return;
            _backgroundImg.sprite = _selectedBackgroundSprite;
            _iconImg.sprite = _selectedSprite;
            _iconImg.SetNativeSize();
            _tabRect.sizeDelta = _selectedRectSize;
            _page.SetActive(true);
        }

        public void Deselect()
        {
            if (this != _tabGroup.SelectedTab) return;
            _backgroundImg.sprite = _deselectedBackgroundSprite;
            _iconImg.sprite = _deselectedSprite;
            _iconImg.SetNativeSize();
            _tabRect.sizeDelta = _deselectedRectSize;
            _page.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _tabGroup.SelectTab(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }
    }
}