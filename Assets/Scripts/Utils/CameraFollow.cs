using System;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private Transform _target;
        
        // [SerializeField]
        // [Range(0.2f, 2f)]
        // private float _speed;
        
        [SerializeField]
        [Range(1f, 10f)]
        private float _speed;

        private Transform _transform;

        private float _velocity;

        private void Awake()
        {
            _transform = transform;
            _velocity = _speed / 100;
        }

        private void Update()
        {
            var position = _target.position;
            _transform.position = Vector3.Lerp(_transform.position, new Vector3(position.x, position.y, -10), _velocity);
            // _transform.position = Vector3.SmoothDamp(_transform.position, new Vector3(position.x, position.y, -10), ref _velocity, 1f);
            // _transform.position = new Vector3(position.x, position.y, -10);
        }
    }
}
