using System;
using System.Collections;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Utils;
using Pathfinding;
using UnityEngine;
using UnityEngine.Serialization;

namespace ChiciStudios.ProjectPhoenix.AI
{
    public class AIMovementController : MonoBehaviour
    {
        [field: SerializeField]
        public Transform FollowTarget { get; set; }
        
        [SerializeField, RequireInterface(typeof(IAttackCommandActor))]
        public UnityEngine.Object actor;
        private IMoveCommandActor _actor;
        
        [SerializeField]
        [Tooltip("Setting this allows sprites to be flipped based on travel direction.")]
        private Transform _sprite;

        [SerializeField]
        [Range(0.1f, 1f)]
        private float _trackingSpeed;

        [SerializeField]
        [Range(0.2f, 2f)]
        private float _regeneratePathRate;

        [SerializeField]
        [Range(0.05f, 5f)]
        private float _nextWaypointDistance;

        [SerializeField]
        [Range(0.2f, 2f)]
        private float _detectRadius;

        private Path _path;
        private Seeker _seeker;
        private Rigidbody2D _rb;
        private int _currentWaypoint;
        private bool _reachedEndOfPath;
        private bool _shouldFlipSprite;
        private IEnumerator _generatePathCoroutine;
        private ICommand _moveCommand;

        private void Start()
        {
            _shouldFlipSprite = _sprite != null;
            _seeker = GetComponent<Seeker>();
            _seeker.pathCallback += OnPathComplete;
            _rb = GetComponent<Rigidbody2D>();
            if (FollowTarget == null)
            {
                Debug.LogWarning("Target not set. Attempting to find player in scene");
                FollowTarget = GameObject.FindWithTag("Player")?.transform;
            }

            if (FollowTarget == null)
            {
                Debug.LogWarning("Player not found in scene. Aborting AIMovement");
                Destroy(this);
                return;
            }

            _actor = actor as IMoveCommandActor;
            _moveCommand = new MoveCommand();
        }

        private void FixedUpdate()
        {

            var inRange = Vector3.Distance(_rb.position, FollowTarget.position) <= _detectRadius;

            if (!inRange)
            {
                // Debug.Log("Preventing move because out of range");
                if (_generatePathCoroutine != null) StopCoroutine(_generatePathCoroutine);
                _generatePathCoroutine = null;
                return;
            }

            if (_generatePathCoroutine == null)
            {
                // Debug.Log("Preventing move because no generated path");
                _generatePathCoroutine = RecursivelyGeneratePath();
                StartCoroutine(_generatePathCoroutine);
            }

            if (_path == null)
            {
                // Debug.Log("Preventing move because path is null");
                return;
            }

            while (true)
            {
                var distanceToWaypoint = Vector3.Distance(transform.position, _path.vectorPath[_currentWaypoint]);
                if (distanceToWaypoint < _nextWaypointDistance)
                {
                    // Check if there is another waypoint or if we have reached the end of the path
                    if (_currentWaypoint + 1 < _path.vectorPath.Count)
                    {
                        _currentWaypoint++;
                    }
                    else
                    {
                        _reachedEndOfPath = true;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }

            if (_reachedEndOfPath)
            {
                // Debug.Log($"Preventing move because end of path is reached. VectorPathCount: {_path.vectorPath.Count}");
                return;
            }

            Move();
        }

        private void Move()
        {
            var direction = ((Vector2)_path.vectorPath[_currentWaypoint] - _rb.position).normalized;
            _actor.Move(direction * (_trackingSpeed * Time.fixedDeltaTime));
        }

        private IEnumerator RecursivelyGeneratePath()
        {
            if (_seeker.IsDone())
            {
                _seeker.StartPath(_rb.position, FollowTarget.position);
                yield return new WaitForSeconds(_regeneratePathRate);
            }

            yield return null;
            _generatePathCoroutine = RecursivelyGeneratePath();
            StartCoroutine(_generatePathCoroutine);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _detectRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _nextWaypointDistance);
        }

        private void OnPathComplete(Path p)
        {
            if (p.error) return;
            _path = p;
            _currentWaypoint = 1;
            _reachedEndOfPath = false;
        }

        private void OnDestroy()
        {
            _seeker.pathCallback -= OnPathComplete;
        }
    }
}