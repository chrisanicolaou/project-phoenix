using System;
using ChiciStudios.ProjectPhoenix.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Inventory
{
    public class ItemSlot : MonoBehaviour
    {
        [SerializeField]
        private Sprite _lockedSlotSprite;
        
        [SerializeField]
        private Sprite _unoccupiedSlotSprite;
        
        [SerializeField]
        private Sprite _occupiedSlotSprite;
        
        [SerializeField]
        private Image _slotImg;
        
        [SerializeField]
        private Image _itemImg;

        [SerializeField]
        private TextMeshProUGUI _quantityText;

        private QuantifiableItem _qItem;

        public void LockSlot()
        {
            _slotImg.sprite = _lockedSlotSprite;
            _quantityText.enabled = false;
            _itemImg.enabled = false;
        }

        public void UnlockSlot()
        {
            _slotImg.sprite = _unoccupiedSlotSprite;
            _quantityText.enabled = false;
            _itemImg.enabled = false;
        }

        public void Populate(QuantifiableItem qItem)
        {
            _qItem = qItem;
            _slotImg.sprite = _occupiedSlotSprite;
            _itemImg.enabled = true;
            _itemImg.sprite = _qItem.Item.Sprite;
            _quantityText.enabled = true;
            _quantityText.text = qItem.Quantity.ToString();
        }
    }
}
