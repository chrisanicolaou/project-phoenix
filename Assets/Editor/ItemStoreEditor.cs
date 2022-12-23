using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Items;
using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.Questing.Steps;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Editor
{
    [CustomEditor(typeof(ItemStore))]
    public class ItemStoreEditor : UnityEditor.Editor
    {
        private ItemStore _target;

        private void OnEnable()
        {
            _target = target as ItemStore;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            if (GUILayout.Button("Clear"))
            {
                _target.Clear();
            }
        }
    }
}