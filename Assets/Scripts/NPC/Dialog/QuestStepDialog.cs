using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    [Serializable]
    public class QuestStepDialog
    {
        [field: SerializeField]
        public int StepIndex { get; set; }
        
        [field: SerializeField]
        public string[] StepStartingDialog { get; set; }
        
        [field: SerializeField]
        public string[] StepIncompleteDialog { get; set; }
        
        [field: SerializeField]
        public string[] StepCompleteDialog { get; set; }
    }
}