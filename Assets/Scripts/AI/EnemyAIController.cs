using System;
using System.Collections;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.ProjectPhoenix.AI
{
    public class EnemyAIController : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;

        [SerializeField, RequireInterface(typeof(IAttackCommandActor))]
        public UnityEngine.Object actor;
        private IAttackCommandActor _actor;
        
        [SerializeField]
        [Range(0.2f, 5f)]
        private float _attackRate;

        [SerializeField]
        [Range(0.05f, 5f)]
        private float _attackRange;
        
        private AIMovementController _movementController;

        private IEnumerator _attackCoroutine;

        private Animator _animator;

        private AttackCommand _attackCommand;

        private Transform _transform;

        private void Awake()
        {
            if (_target == null)
            {
                Debug.LogWarning("Target not set. Attempting to find player in scene");
                _target = GameObject.FindWithTag("Player")?.transform;
            }

            if (_target == null)
            {
                Debug.LogWarning("Player not found in scene. Aborting AIMovement");
                Destroy(this);
                return;
            }
            
            _actor = actor as IAttackCommandActor;
            _movementController = GetComponent<AIMovementController>();
            _movementController.FollowTarget = _target;
            _attackCommand = new AttackCommand();
            _transform = transform;
        }

        private void Update()
        {
            if (InRange() && _attackCoroutine == null)
            {
                Debug.Log("In range - starting attack!");
                _attackCoroutine = Attack();
                StartCoroutine(_attackCoroutine);
            }
        }

        private bool InRange()
        {
            return Vector3.Distance(_transform.position, _target.position) <= _attackRange;
        }

        private IEnumerator Attack()
        {
            _attackCommand.Execute(_actor);
            yield return new WaitForSeconds(_attackRate);
            _attackCoroutine = null;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
        }
    }
}