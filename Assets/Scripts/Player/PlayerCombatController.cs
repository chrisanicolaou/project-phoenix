using System;
using ChiciStudios.ProjectPhoenix.Combat;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Player
{
    public class PlayerCombatController : MonoBehaviour, IAttackCommandActor, IHitboxReceiver, IHurtboxReceiver
    {
        // This will eventually be factored away
        [SerializeField]
        private int _damage = 10;
        [SerializeField]
        private HitType _hitType = HitType.Melee;
        private HitInfo _defaultHitInfo;
        // to a separate class that is attached to weapons
        
        [SerializeField]
        private PlayerController _controller;

        [SerializeField]
        private HitboxRegister _hitboxRegister;

        [SerializeField]
        private Animator _animator;

        private void Awake()
        {
            _defaultHitInfo = new HitInfo(_damage, _hitType);
        }

        public void ProcessHits()
        {
            var hits = _hitboxRegister.RegisterAttack();
            foreach (var hurtbox in hits)
            {
                hurtbox.RegisterHit(_defaultHitInfo);
            }
        }

        public void Attack()
        {
            _controller.IsBusy = true;
            // _animator.Play(PhoenixAnimationType.Attack);
            _animator.SetBool("isAttacking", true);
        }

        public void OnHit(HitInfo hit)
        {
            _animator.SetTrigger("hit");
            _controller.IsBusy = true;
            _controller.Health.Value -= hit.Damage;
        }

        public void StopHitAnimation()
        {
            _controller.IsBusy = false;
        }

        public void StopAttackAnimation()
        {
            _animator.SetBool("isAttacking", false);
            _controller.IsBusy = false;
        }
    }
}