using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [Serializable]
    public class QuantifiableItem
    {
        [field: SerializeField]
        public Item Item { get; set; }
        
        [field: SerializeField]
        public int Quantity { get; set; }

        public QuantifiableItem(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}