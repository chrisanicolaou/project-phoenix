using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.VariableSO;
using UnityEditor;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Editor
{
    [CustomEditor(typeof(IntVariable))]
    public class IntVariableEditor : UnityEditor.Editor
    {
        private IntVariable _target;
        
        private void OnEnable()
        {
            _target = target as IntVariable;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Fire Event"))
            {
                _target.ValueChanged?.Invoke();
            }
            if (GUILayout.Button("Reset Event"))
            {
                _target.ValueChanged = null;
            }
        }
    }
}