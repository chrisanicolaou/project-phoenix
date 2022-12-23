using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Settings
{
    [CreateAssetMenu(fileName = "NewInputMap", menuName = "ScriptableObjects/Settings/InputMap", order = 1)]
    public class InputMap : ScriptableObject
    {
        
        public Keymap KeyMap;
        
        [field: SerializeField]
        public Axis HorizontalAxisKeys { get; private set; } = new Axis();
        
        [field: SerializeField]
        public Axis VerticalAxisKeys { get; private set; } = new Axis();
    }
}