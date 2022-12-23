using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Enemies
{
    public class CreepCampController : MonoBehaviour
    {
        private static Transform _playerTransform;

        [SerializeField]
        private GameObject _creepPrefab;

        [SerializeField]
        [Range(1, 50)]
        private int _campSize;
        
        [SerializeField]
        [Range(15, 50)]
        private int _respawnTimer;

        private Transform _transform;

        private void Awake()
        {
            _playerTransform ??= GameObject.FindWithTag("Player").transform;
            _transform = transform;
        }

        private void Update()
        {
            if (transform.childCount >= _campSize) return;

            StartCoroutine(SpawnCreep());
        }

        private IEnumerator SpawnCreep()
        {
            var creep = Instantiate(_creepPrefab, _transform);
            creep.SetActive(false);
            yield return new WaitForSeconds(_respawnTimer);
            creep.SetActive(true);
        }
    }
}
