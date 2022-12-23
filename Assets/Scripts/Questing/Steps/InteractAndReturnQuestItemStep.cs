using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Items;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing.Steps
{
    [Serializable]
    [Tooltip("Must follow a 'Gather' step with the same Item ID!")]
    public class InteractAndReturnQuestItemStep : InteractStep
    {
        [field: SerializeField]
        [Tooltip("Must be equal to the ID from the previous 'Gather' step!")]
        public int ItemId { get; set; }
        
        [field: SerializeField]
        [Tooltip("Should be equal to the amount from the previous 'Gather' step!")]
        public int AmountToReturn { get; set; }

        [SerializeField]
        private ItemStore _inventory;

        public override void OnInteractionComplete(Dictionary<string, object> msg)
        {
            base.OnInteractionComplete(msg);
            _inventory.RemoveById(ItemId, AmountToReturn);
        }
    }
}