using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils
{
    [Serializable]
    public class Axis
    {
        [field: SerializeField]
        public KeyCode Positive { get; set; }
        
        [field: SerializeField]
        public KeyCode Negative { get; set; }
    }
}