using System;
using ChiciStudios.ProjectPhoenix.Questing;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    [Serializable]
    public class SubscribedQuests
    {
        [field: SerializeField]
        public Quest Quest { get; set; }
        
        [field: SerializeField]
        public QuestStepDialog[] Steps { get; set; }
    }
}