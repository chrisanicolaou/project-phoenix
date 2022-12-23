using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enemies;
using ChiciStudios.ProjectPhoenix.Inputs;
using Unity.VisualScripting;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Player
{
    public class PlayerMovementController : MonoBehaviour, IAttackCommandActor, IRollCommandActor, IMoveCommandActor
    {
        [SerializeField]
        private PlayerController _controller;
        
        [SerializeField]
        private Rigidbody2D _rb;

        // [SerializeField] private PhoenixAnimationController _animator;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        [Range(0.5f, 3f)]
        private float _speed;

        [SerializeField]
        private float _rollForce;

        [SerializeField]
        private float _rollDuration;

        private Vector2 _previousMovement;
        private bool _isRegisteringAttacks;

        void Start()
        {
            _rb ??= GetComponent<Rigidbody2D>();
            if (_rb == null) Debug.LogError("No Rigidbody found on player!");
            _previousMovement = Vector2.right;
        }

        private void LateUpdate()
        {
            _animator.SetFloat("speed", 0);
        }

        public void Move(Vector2 targetPosition)
        {
            _previousMovement = targetPosition;
            var absMovement = Mathf.Abs(targetPosition.x) + Mathf.Abs(targetPosition.y);
            _animator.SetFloat("speed", absMovement);
            var yRotation = targetPosition.x switch
            {
                > 0 => 180,
                < 0 => 0,
                _ => transform.rotation.eulerAngles.y
            };
            if (Math.Abs(yRotation - transform.rotation.eulerAngles.y) > 0.001)
            {
                transform.rotation = Quaternion.Euler(0, yRotation, 0);
            }

            _rb.MovePosition(_rb.position + targetPosition.normalized * (_speed * Time.fixedDeltaTime));
        }

        public void Roll()
        {
            StartCoroutine(RollInternal());
        }

        public void Attack()
        {
            _controller.IsBusy = true;
            // _animator.Play(PhoenixAnimationType.Attack);
            _animator.SetBool("isAttacking", true);
        }

        public void Die()
        {
            throw new NotImplementedException();
        }

        private IEnumerator RollInternal()
        {
            _controller.IsBusy = true;
            // _animator.Play(PhoenixAnimationType.Roll);
            var remainingDuration = _rollDuration;
            var rollDirection = _previousMovement;
            while (remainingDuration > 0)
            {
                var time = Time.fixedDeltaTime;
                _rb.MovePosition(_rb.position + rollDirection.normalized * (_rollForce * time));
                remainingDuration -= time;
                yield return new WaitForSeconds(time);
            }

            _controller.IsBusy = false;
        }

        // public void RegisterAttack()
        // {
        //     _attackHitbox.enabled = true;
        //     var results = new List<Collider2D>();
        //     _attackHitbox.OverlapCollider(new ContactFilter2D().NoFilter(), results);
        //     var enemies = results.Where(c => c.CompareTag("Enemy")).DistinctBy(c => c.gameObject.GetInstanceID());
        //     foreach (var enemy in enemies)
        //     {
        //         enemy.gameObject.GetComponentInParent<BaseEnemy>().Health -= 10;
        //     }
        // }
        //
        // public void UnregisterAttack()
        // {
        //     _attackHitbox.enabled = false;
        // }
        //
        // public void StopAttackAnimation()
        // {
        //     _animator.SetBool("isAttacking", false);
        //     _controller.IsBusy = false;
        // }

        public void ToggleImmunity(bool toggle)
        {
            var targetLayer = toggle ? "ImmuneState" : "Character";
            gameObject.layer = LayerMask.NameToLayer(targetLayer);
        }
    }
}