using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.VariableSO
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = "ScriptableObjects/Variables/Int", order = 1)]
    public class IntVariable : SOVariable<int>
    {
        [SerializeField]
        private bool _minimumZeroValue;
        public override int Value
        {
            get => _value;
            set
            {
                {
                    _value = _minimumZeroValue ? Mathf.Max(0, value) : value;
                    ValueChanged?.Invoke();
                }
            }
        }
    }
}