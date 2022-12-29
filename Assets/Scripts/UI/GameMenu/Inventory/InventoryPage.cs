using System;
using ChiciStudios.ProjectPhoenix.Items;
using ChiciStudios.ProjectPhoenix.UI.Utils;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;
using UnityEngine.UI;

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
        
        [SerializeField]
        private NonUpdatingGridView _gridView;

        private void OnEnable()
        {
            PopulateItemSlots();
        }

        private void OnDisable()
        {
            // GetComponent<GridLayoutGroup>().enabled = true;
        }

        private void Update()
        {
        }

        public void PopulateItemSlots()
        { 
            _itemSlotContainer.DestroyAllChildren();
            _itemSlots = new ItemSlot[_inventoryStore.MaxCapacity];
            for (var i = 0; i < _inventoryStore.MaxCapacity; i++)
            {
                var itemSlotObj = Instantiate(_itemSlotPrefab, _itemSlotContainer, false);
                _itemSlots[i] = itemSlotObj.GetComponent<ItemSlot>();
                _itemSlots[i].InventoryIndex = i;
                _itemSlots[i].ItemStore = _inventoryStore;
                _itemSlots[i].Page = this;
                _itemSlots[i].UnlockSlot();
                if (_inventoryStore.Items[i].Item != null) _itemSlots[i].Populate(_inventoryStore.Items[i]);
            }
            _gridView.CreateGrid(transform);
        }
    }
}