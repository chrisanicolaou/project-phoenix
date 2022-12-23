using System;
using ChiciStudios.ProjectPhoenix.Questing;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    [Serializable]
    public class QuestActivation
    {
        [field: SerializeField]
        public Quest Quest { get; set; }
        
        [field: SerializeField]
        public string[] ActivationDialogs { get; set; }
    }
}