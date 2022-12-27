using UnityEngine;
using UnityEngine.EventSystems;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Inventory
{
    public class ItemDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform _rectTransform;
        private Vector2 _originalPos;
        private Vector2 _currentPos;
        private int _siblingIndex;
        private CanvasGroup _canvasGroup;
        private Canvas _canvas;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _rectTransform ??= GetComponent<RectTransform>();
            _canvasGroup ??= GetComponent<CanvasGroup>();
            _canvas ??= GetComponentInParent<Canvas>();
            var anchoredPosition = _rectTransform.anchoredPosition;
            _originalPos = anchoredPosition;
            _currentPos = anchoredPosition;
            var parent = _rectTransform.parent;
            _siblingIndex = parent.GetSiblingIndex();
            parent.SetAsLastSibling();
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _currentPos += eventData.delta / _canvas.scaleFactor;
            _rectTransform.anchoredPosition = _currentPos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _rectTransform.parent.SetSiblingIndex(_siblingIndex);
            _canvasGroup.blocksRaycasts = true;
            _rectTransform.anchoredPosition = _originalPos;
            _currentPos = _originalPos;
        }
    }
}