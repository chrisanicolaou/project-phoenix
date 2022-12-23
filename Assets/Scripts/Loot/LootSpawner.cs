using System;
using System.Collections.Generic;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Items;
using ChiciStudios.ProjectPhoenix.Questing;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ChiciStudios.ProjectPhoenix.Loot
{
    public class LootSpawner : MonoBehaviour
    {
        [SerializeField]
        private QuestStore _activeQuestsStore;
        
        [SerializeField]
        private GameObject _lootPrefab;

        [SerializeField]
        private LootSpawnInfo[] _lootToSpawn;

        [SerializeField]
        private LootSpawnInfo[] _questItemsToSpawn;

        [SerializeField]
        [Range(0.01f, 0.2f)]
        private float _dropSpread;

        private Transform _transform;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position, _dropSpread);
        }

        private void Start()
        {
            _transform = transform;
        }

        public void SpawnLoot()
        {
            var lootTransforms = new List<Transform>();

            foreach (var lootSpawnInfo in _lootToSpawn)
            {
                var lootTransform = CreateLoot(lootSpawnInfo);
                if (lootTransform != null) lootTransforms.Add(lootTransform);
            }

            foreach (var lootSpawnInfo in _questItemsToSpawn)
            {
                if (lootSpawnInfo.ItemToSpawn is QuestItem questItem)
                {
                    var activeQuest = _activeQuestsStore.Quests.Select(q => q)
                        .Zip(questItem.Quests,
                            (q, qi) => q.Id == qi.QuestId && q.CurrentStepIndex == qi.StepIndex ? q : null)
                        .FirstOrDefault();
                    
                    if (activeQuest == null) continue;
                    
                    var lootTransform = CreateLoot(lootSpawnInfo);
                    if (lootTransform != null) lootTransforms.Add(lootTransform);
                }
            }

            if (lootTransforms.Count == 0) return;

            var points = PlotCirclePoints(lootTransforms.Count, _dropSpread, _transform.position);

            for (int i = 0; i < lootTransforms.Count; i++)
            {
                lootTransforms[i].position = points[i];
            }

            Destroy(this);
        }

        private Transform CreateLoot(LootSpawnInfo lootSpawnInfo)
        {
            var chance = Random.value;
            if (chance > lootSpawnInfo.SpawnChance) return null;

            var lootObj = Instantiate(_lootPrefab);
            lootObj.GetComponent<SpriteRenderer>().sprite = lootSpawnInfo.ItemToSpawn.Sprite;
            var loot = lootObj.GetComponent<Loot>();
            loot.Item = lootSpawnInfo.ItemToSpawn;
            loot.MinAmount = lootSpawnInfo.MinAmount;
            loot.MaxAmount = lootSpawnInfo.MaxAmount;
            var lootTransform = lootObj.transform;
            lootTransform.position = _transform.position;

            return lootTransform;
        }

        private Vector2[] PlotCirclePoints(int numOfPoints, float radius, Vector2 center)
        {
            var points = new Vector2[numOfPoints];
            var angleSlice = (360f / numOfPoints) * (Mathf.PI / 180);
            for (var i = 0; i < numOfPoints; i++)
            {
                var angle = angleSlice * i;
                var x = radius * Mathf.Cos(angle) + center.x;
                var y = radius * Mathf.Sin(angle) + center.y;
                points[i] = new Vector2(x, y);
            }

            return points;
        }
    }
}