using System;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Items;
using ChiciStudios.ProjectPhoenix.VariableSO;
using UnityEngine;
// ReSharper disable InconsistentNaming

namespace ChiciStudios.ProjectPhoenix.Questing
{
    [Serializable]
    public class QuestReward
    {
        public bool GoldReward { get; set; }
        
        public IntVariable GoldVariable { get; set; }
        
        public int GoldAmount { get; set; }
        
        public bool ItemReward { get; set; }
        
        public ItemStore InventoryStore { get; set; }
        
        [field: SerializeField]
        public QuantifiableItem[] Items { get; set; }

        public bool TryIssueReward()
        {
            if (ItemReward)
            {
                var intersectingIds = InventoryStore.Items.Select(qi => qi.Item.Id)
                    .Intersect(Items.Select(qi => qi.Item.Id));
                if (InventoryStore.Items.Length + Items.Length - intersectingIds.Count() > InventoryStore.MaxCapacity) return false;
                foreach (var qItem in Items)
                {
                    InventoryStore.TryAdd(qItem);
                }
            }
            
            if (GoldReward) GoldVariable.Value += GoldAmount;

            return true;
        }
    }
}