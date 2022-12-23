using System;
using ChiciStudios.ProjectPhoenix.Items;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Loot
{
    [Serializable]
    public class LootSpawnInfo
    {
        [field: SerializeField]
        public Item ItemToSpawn { get; set; }
        
        [field: SerializeField]
        public int MinAmount { get; set; }
        
        [field: SerializeField]
        public int MaxAmount { get; set; }
        
        [field: SerializeField]
        [field: Range(0.001f, 1f)]
        public float SpawnChance { get; set; }
    }
}