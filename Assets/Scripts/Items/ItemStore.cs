using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [CreateAssetMenu(fileName = "NewItemStore", menuName = "ScriptableObjects/Items/ItemStore", order = 1)]
    public class ItemStore : ScriptableObject
    {
        private QuantifiableItem[] _items;

        public QuantifiableItem[] Items => _items ??= new QuantifiableItem[MaxCapacity];

        [field: SerializeField]
        public int MaxCapacity { get; set; } = 10;

        public bool TryAdd(QuantifiableItem qItem)
        {
            var i = Array.FindIndex(Items, q => q != null && q.Item.Id == qItem.Item.Id);
            
            if (i == -1)
            {
                return AddNew(qItem);
            }

            Items[i].Quantity += qItem.Quantity;
            return true;
        }

        private bool AddNew(QuantifiableItem qItem)
        {
            var i = Array.FindIndex(Items, q => q == null);
            if (i == -1) return false;
            Items[i] = qItem;
            return true;
        }

        public void RemoveById(int id, int quantity)
        {
            var i = Array.FindIndex(Items, q => q != null && q.Item.Id == id);
            
            if (i == -1)
            {
                Debug.LogWarning($"Trying to remove non-existent item from store: {name}. Item ID: {id}");
                return;
            }

            if (quantity > Items[i].Quantity)
            {
                Debug.LogWarning($"Trying to remove more of the item than exists in store: {name}. Item: {i}");
                return;
            }

            Items[i].Quantity -= quantity;

            if (Items[i].Quantity == 0)
            {
                Items[i] = null;
            }
        }

        public void Remove(QuantifiableItem qItem)
        {
            RemoveById(qItem.Item.Id, qItem.Quantity);
        }

        # if UNITY_EDITOR
        public void Clear()
        {
            _items = new QuantifiableItem[MaxCapacity];
        }
        #endif
    }
}