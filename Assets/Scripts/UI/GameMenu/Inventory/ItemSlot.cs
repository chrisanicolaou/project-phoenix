using System;
using ChiciStudios.ProjectPhoenix.Items;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Inventory
{
    public class ItemSlot : MonoBehaviour, IDropHandler
    {
        public ItemStore ItemStore { get; set; }
        public InventoryPage Page { get;set; }
    
        [SerializeField] private Sprite _lockedSlotSprite;

        [SerializeField] private Sprite _unoccupiedSlotSprite;

        [SerializeField] private Sprite _occupiedSlotSprite;

        [SerializeField] private Image _slotImg;

        [SerializeField] private Image _itemImg;

        [SerializeField] private TextMeshProUGUI _quantityText;

        public int InventoryIndex { get; set; }
        public QuantifiableItem QItem { get; set; }

        public void LockSlot()
        {
            _slotImg.sprite = _lockedSlotSprite;
            _quantityText.enabled = false;
            _itemImg.gameObject.SetActive(false);
        }

        public void UnlockSlot()
        {
            _slotImg.sprite = _unoccupiedSlotSprite;
            _quantityText.enabled = false;
            _itemImg.gameObject.SetActive(false);
        }

        public void Populate(QuantifiableItem qItem)
        {
            QItem = qItem;
            _slotImg.sprite = _occupiedSlotSprite;
            _itemImg.gameObject.SetActive(true);
            _itemImg.sprite = QItem.Item.Sprite;
            _quantityText.enabled = true;
            _quantityText.text = qItem.Quantity.ToString();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggedItemSlot = eventData.pointerDrag.GetComponentInParent<ItemSlot>();
            if (draggedItemSlot == null) return;

            (ItemStore.Items[InventoryIndex], ItemStore.Items[draggedItemSlot.InventoryIndex]) = 
            (ItemStore.Items[draggedItemSlot.InventoryIndex], ItemStore.Items[InventoryIndex]);
            Page.PopulateItemSlots();
        }
    }
}