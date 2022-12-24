using System;
using ChiciStudios.ProjectPhoenix.Items;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField]
        private ItemSlot[] _itemSlots;

        [SerializeField]
        private ItemStore _inventoryStore;

        private int _inventorySize;

        private void OnEnable()
        {
            PopulateItemSlots();
        }

        private void Update()
        {
            if (_inventorySize != _inventoryStore.Items.Count) PopulateItemSlots();
        }

        private void PopulateItemSlots()
        {
            _inventorySize = _inventoryStore.Items.Count;
            for (var i = 0; i < _itemSlots.Length; i++)
            {
                if (i >= _inventoryStore.MaxCapacity)
                {
                    _itemSlots[i].LockSlot();
                    continue;
                }
                
                _itemSlots[i].UnlockSlot();
                if (i < _inventorySize) _itemSlots[i].Populate(_inventoryStore.Items[i]);
            }
        }
    }
}