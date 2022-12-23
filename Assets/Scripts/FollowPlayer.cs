// using System;
// using UnityEngine;
//
// namespace ChiciStudios.ProjectPhoenix
// {
//     public class FollowPlayer : MonoBehaviour
//     {
//         public bool Freeze { get; set; }
//
//         [SerializeField]
//         [Range(0.2f, 2f)]
//         private float _detectRadius;
//         
//         [SerializeField]
//         private Rigidbody2D _rb;
//
//         [SerializeField]
//         [Range(0.2f, 0.5f)]
//         private float _speed;
//
//         [SerializeField]
//         [Range(0.01f, 1f)]
//         private float _deadZoneAmount;
//
//         private bool _isFollowing;
//
//         private Transform _transform;
//
//         private Transform _player;
//
//         private Vector2 _direction;
//
//         private void Awake()
//         {
//             _transform = _rb.transform;
//         }
//
//         private void OnDrawGizmosSelected()
//         {
//             Gizmos.color = Color.yellow;
//             Gizmos.DrawWireSphere(transform.position, _detectRadius);
//         }
//
//         private void LateUpdate()
//         {
//             if (!_isFollowing || Freeze) return;
//
//             _direction = Vector2.zero;
//
//             if (!(Mathf.Abs(_player.position.x - _transform.position.x) < _deadZoneAmount))
//             {
//                 _direction += _player.position.x > _transform.position.x ? Vector2.right : Vector2.left;
//                 _transform.rotation = Quaternion.Euler(0f, _player.position.x > _transform.position.x + _deadZoneAmount ? 180f : 0f, 0f);
//             }
//
//             if (!(Mathf.Abs(_player.position.y - _transform.position.y) < _deadZoneAmount))
//             {
//                 _direction += _player.position.y > _transform.position.y ? Vector2.up : Vector2.down;
//             }
//
//             _rb.MovePosition(_rb.position + _direction.normalized * (_speed * Time.fixedDeltaTime));
//         }
//     }
// }