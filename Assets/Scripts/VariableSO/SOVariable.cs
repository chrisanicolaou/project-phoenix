using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.VariableSO
{
    public class SOVariable<T> : ScriptableObject
    {
        public Action ValueChanged;
        
        [SerializeField]
        protected T _value;

        public virtual T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }
    }
}