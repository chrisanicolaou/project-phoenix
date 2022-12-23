using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Inputs;
using ChiciStudios.ProjectPhoenix.Utils;
using ChiciStudios.ProjectPhoenix.VariableSO;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Player
{
    public class PlayerController : MonoBehaviour, IInteractCommandActor
    {
        [SerializeField, RequireInterface(typeof(IMoveCommandActor))]
        private UnityEngine.Object _moveCommandActorObject;
        private IMoveCommandActor _moveCommandActor;
        
        [SerializeField, RequireInterface(typeof(IRollCommandActor))]
        private UnityEngine.Object _rollCommandActorObject;
        private IRollCommandActor _rollCommandActor;

        [SerializeField, RequireInterface(typeof(IAttackCommandActor))]
        private UnityEngine.Object _attackCommandActorObject;
        private IAttackCommandActor _attackCommandActor;

        // Tba once interactions are separated out
        // [SerializeField, RequireInterface(typeof(IInteractCommandActor))]
        // private UnityEngine.Object _interactCommandActorObject;
        // private IInteractCommandActor _interactCommandActor;

        [SerializeField]
        private InputHandler _input;
        
        [SerializeField]
        private Animator _animator;

        [field: SerializeField]
        public IntVariable MaxHealth { get; set; }

        [field: SerializeField]
        public IntVariable Health { get; set; }

        [SerializeField]
        private Vector2Variable _spawnPosition;

        [SerializeField]
        private float _interactRadius = 0.25f;

        [SerializeField]
        private GameEvent _playerDeathEvent;
        
        public bool IsBusy { get; set; }
        private Transform _transform;
        private Vector2 _movement;
        private bool _isDead;

        private void Start()
        {
            _transform = transform;
            _moveCommandActor = _moveCommandActorObject as IMoveCommandActor;
            _rollCommandActor = _rollCommandActorObject as IRollCommandActor;
            _attackCommandActor = _attackCommandActorObject as IAttackCommandActor;
            Health.ValueChanged += CheckDeath;
            _spawnPosition.Value = _transform.position;
        }

        private void Update()
        {
            if (Time.timeScale == 0f || _isDead) return;
            
            _movement = Vector2.zero;

            if (IsBusy || !Input.anyKey) return;

            _movement = _input.GetAxisRaw();

            if (IsBusy || !Input.anyKeyDown) return;
            
            var command = _input.HandleInput();
            
            ExecuteCommand(command);
        }

        private void LateUpdate()
        {
            if (IsBusy || _isDead || Time.timeScale == 0f || _movement == Vector2.zero) return;
            _moveCommandActor.Move(_movement);
        }

        public void Interact()
        {
            var nearbyColliders = Physics2D.OverlapCircleAll(_transform.position, _interactRadius)
                .Where(c => !c.CompareTag(tag))
                .OrderBy(c => Vector2.Distance(_transform.transform.position, c.transform.position))
                .ToArray();
            for (int i = 0; i < nearbyColliders.Length; i++)
            {
                var interactable =
                    nearbyColliders[i].GetComponent(typeof(IInteractCommandActor)) as IInteractCommandActor;
                if (interactable != null)
                {
                    interactable.Interact();
                    return;
                }
            }

            Debug.Log("No interactables found nearby!");
        }

        private void CheckDeath()
        {
            if (Health.Value <= 0 && !_isDead) Die();
        }

        private void Die()
        {
            _isDead = true;
            _playerDeathEvent.Fire(new Dictionary<string, object>());
            IsBusy = true;
            _animator.SetBool("isDead", true);
        }

        public void OnDeathAnimationComplete()
        {
            
        }

        public void Respawn()
        {
            _animator.SetBool("isDead", false);
            // _animator.WriteDefaultValues();
            _transform.position = _spawnPosition.Value;
            _isDead = false;
        }

        private void ExecuteCommand(ICommand command)
        {
            switch (command)
            {
                case null:
                    return;
                case AttackCommand:
                    command.Execute(_attackCommandActor);
                    break;
                case MoveCommand:
                    command.Execute(_moveCommandActor);
                    break;
                case RollCommand:
                    command.Execute(_rollCommandActor);
                    break;
                case InteractCommand:
                    command.Execute(this);
                    break;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _interactRadius);
        }
    }
}