using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [CreateAssetMenu(fileName = "NewItemStore", menuName = "ScriptableObjects/Items/ItemStore", order = 1)]
    public class ItemStore : ScriptableObject
    {
        public List<QuantifiableItem> Items { get; private set; } = new List<QuantifiableItem>();

        [field: SerializeField]
        public int MaxCapacity { get; set; } = 10;

        public bool TryAdd(QuantifiableItem qItem)
        {
            var existingItem = Items.FirstOrDefault(i => i.Item.Id == qItem.Item.Id);
            
            if (existingItem == null)
            {
                return AddNew(qItem);
            }

            existingItem.Quantity += qItem.Quantity;
            return true;
        }

        private bool AddNew(QuantifiableItem qItem)
        {
            if (Items.Count == MaxCapacity) return false;
            
            Items.Add(qItem);
            return true;
        }

        public void RemoveById(int id, int quantity)
        {
            var existingItem = Items.FirstOrDefault(i => i.Item.Id == id);
            
            if (existingItem == null)
            {
                Debug.LogWarning($"Trying to remove non-existent item from store: {name}. Item ID: {id}");
                return;
            }

            if (quantity > existingItem.Quantity)
            {
                Debug.LogWarning($"Trying to remove more of the item than exists in store: {name}. Item: {existingItem}");
                return;
            }

            existingItem.Quantity -= quantity;

            if (existingItem.Quantity == 0)
            {
                Items.Remove(existingItem);
            }
        }

        public void Remove(QuantifiableItem qItem)
        {
            RemoveById(qItem.Item.Id, qItem.Quantity);
        }

        # if UNITY_EDITOR
        public void Clear()
        {
            Items.Clear();
        }
        #endif
    }
}