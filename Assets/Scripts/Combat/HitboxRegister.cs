using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Enemies;
using ChiciStudios.ProjectPhoenix.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Combat
{
    public class HitboxRegister : MonoBehaviour
    {
        [SerializeField]
        private HurtboxTarget[] _targets;

        [SerializeField]
        private Collider2D _hitbox;

        private void Start()
        {
            if (_targets != null && _targets.Length != 0) return;

            Debug.LogError($"No targets set on hitbox register for: {gameObject.name}");
            Destroy(this);
        }

        public HurtboxRegister[] RegisterAttack()
        {
            var results = new List<Collider2D>();
            _hitbox.OverlapCollider(new ContactFilter2D().NoFilter(), results);
            var targets = results.Where(c => _targets.Any(target => c.CompareTag(target.ToString())))
                .DistinctBy(c => c.gameObject.GetInstanceID()).Select(c => c.GetComponent<HurtboxRegister>()).ToArray();
            
            if (!targets.Any())
            {
                Debug.Log("No targets found. If expecting a hit, check the tags on the hurtbox game object match " +
                          "HurtboxTarget tags. Tags searched for:");
                foreach (var target in _targets)
                {
                    Debug.Log(target);
                }
            }

            return targets;
        }
    }
}