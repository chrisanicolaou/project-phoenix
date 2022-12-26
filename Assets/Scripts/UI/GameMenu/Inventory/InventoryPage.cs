using System;
using ChiciStudios.ProjectPhoenix.Items;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Inventory
{
    public class InventoryPage : MonoBehaviour
    {
        [SerializeField]
        private ItemStore _inventoryStore;
        
        [SerializeField]
        private GameObject _itemSlotPrefab;
        
        [SerializeField]
        private Transform _itemSlotContainer;
        
        private ItemSlot[] _itemSlots;

        private void OnEnable()
        {
            PopulateItemSlots();
        }

        private void Update()
        {
        }

        private void PopulateItemSlots()
        { 
            _itemSlotContainer.DestroyAllChildren();
            _itemSlots = new ItemSlot[_inventoryStore.MaxCapacity];
            for (var i = 0; i < _inventoryStore.MaxCapacity; i++)
            {
                var itemSlotObj = Instantiate(_itemSlotPrefab, _itemSlotContainer, false);
                _itemSlots[i] = itemSlotObj.GetComponent<ItemSlot>();
                _itemSlots[i].UnlockSlot();
                if (_inventoryStore.Items[i] != null) _itemSlots[i].Populate(_inventoryStore.Items[i]);
            }
        }
    }
}