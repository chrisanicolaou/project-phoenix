using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    [Serializable]
    public class VillageLevelDialog
    {
        [field: SerializeField]
        public int VillageLevel { get; set; }
        
        [field: SerializeField]
        public string[] Dialog { get; set; }
    }
}