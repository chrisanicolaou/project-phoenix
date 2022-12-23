using System;
using System.Collections;
using ChiciStudios.ProjectPhoenix.Combat;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Player;
using ChiciStudios.ProjectPhoenix.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.ProjectPhoenix.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour, IMoveCommandActor, IAttackCommandActor, IHitboxReceiver,
        IHurtboxReceiver
    {
        // This will eventually be factored away
        [SerializeField]
        private int _damage;

        [SerializeField]
        private HitType _hitType = HitType.Melee;

        protected HitInfo _defaultHitInfo;
        // to a separate class that is attached to weapons

        [SerializeField]
        protected GameEvent _enemyKilledEvent;

        [SerializeField]
        protected HitboxRegister _hitboxRegister;

        [SerializeField]
        protected Transform _spriteTransform;

        [SerializeField]
        protected Rigidbody2D _rb;

        [FormerlySerializedAs("_healthBar")]
        [SerializeField]
        protected CreepHealthBar _creepHealthBar;

        [SerializeField]
        protected Animator _animator;

        [SerializeField]
        private int _health = 30;

        [field: SerializeField]
        public int Id { get; private set; }

        protected bool _frozen;

        public int Health
        {
            get => _health;
            set
            {
                if (!_creepHealthBar.gameObject.activeSelf)
                {
                    _creepHealthBar.gameObject.SetActive(true);
                    _creepHealthBar.Init(_health);
                }

                UpdateHealthBar(value);
                _health = value;
                if (_health <= 0)
                {
                    Die();
                    return;
                }
            }
        }

        private void Start()
        {
            _defaultHitInfo = new HitInfo(_damage, _hitType);
        }

        protected void UpdateHealthBar(int value)
        {
            _creepHealthBar.UpdateValue(value);
        }

        // Not a bool because unity AnimationEvents do not support bool params for some crazy reason
        protected void SetFreeze(int freeze = 1)
        {
            _frozen = freeze > 0;
            _rb.constraints = freeze > 0 ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.FreezeRotation;
        }

        public abstract void Move(Vector2 distance);

        public abstract void Attack();

        public abstract void Die();

        public abstract void ProcessHits();

        public abstract void OnHit(HitInfo hit);
    }
}