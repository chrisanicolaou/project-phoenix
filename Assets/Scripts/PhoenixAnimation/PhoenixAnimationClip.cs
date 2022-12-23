using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.PhoenixAnimation
{
    [CreateAssetMenu(fileName = "PhoenixAnimationClip", menuName = "ScriptableObjects/PhoenixAnimation/PhoenixAnimationClip", order = 1)]
    public class PhoenixAnimationClip : ScriptableObject
    {
        // [field: SerializeField]
        // public PhoenixAnimationType Type { get; set; }
        
        [field: SerializeField]
        public Sprite[] Frames { get; set; }

        [field: SerializeField]
        public float Duration { get; set; } = 1f;

        [field: SerializeField]
        public bool Loop { get; set; } = true;

        [field: SerializeField]
        public bool PlayOnAwake { get; set; } = false;
    }
}