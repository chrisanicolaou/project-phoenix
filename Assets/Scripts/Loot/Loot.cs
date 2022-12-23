using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Items;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Loot
{
    public class Loot : MonoBehaviour, IInteractCommandActor
    {
        [SerializeField]
        private ItemStore _inventory;
        [SerializeField]
        private GameEvent _lootGatheredEvent;
        public Item Item { get; set; }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
        public virtual void Interact()
        {
            if (Item == null)
            {
                Debug.LogError("No item assigned to loot!");
            }
            if (MaxAmount == 0 || MinAmount > MaxAmount)
            {
                Debug.LogError("Invalid amount initialized for loot!");
            }

            var amount = Random.Range(MinAmount, MaxAmount + 1);

            if (!_inventory.TryAdd(new QuantifiableItem(Item, amount)))
            {
                Debug.LogWarning("Inventory full - cannot add!");
                return;
            }
            
            _lootGatheredEvent.Fire(new Dictionary<string, object>
            {
                { "itemId", Item.Id },
                { "amount", amount }
            });
            
            Destroy(gameObject);
        }
    }
}