using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC.Dialog
{
    [Serializable]
    public class DefaultDialog
    {
        [field: SerializeField]
        public VillageLevelDialog[] VillageLevelDialogs { get; set; }
    }
}