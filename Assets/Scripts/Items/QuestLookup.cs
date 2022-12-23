using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [Serializable]
    public class QuestLookup
    {
        [field: SerializeField]
        public int QuestId { get; set; }
        
        [field: SerializeField]
        public int StepIndex { get; set; }
    }
}