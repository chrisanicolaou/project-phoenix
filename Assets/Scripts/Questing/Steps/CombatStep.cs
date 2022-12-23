using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing.Steps
{
    [Serializable]
    public class CombatStep : QuestStep, IGameEventConsumer
    {
        [SerializeField]
        private GameEvent _enemyKilledEvent;
        
        [field: SerializeField]
        public int EnemyId { get; set; }

        [field: SerializeField]
        public int KillRequirement { get; set; }
        
        public int CurrentKillCount { get; set; }

        public override void Activate()
        {
            base.Activate();
            SubscribeGameEvents();
        }

        private void OnEnemyKilled(Dictionary<string, object> msg)
        {
            if (!_enemyKilledEvent.TryGetValueFromMessage<int>(msg, "enemyId", out var id) || id != EnemyId) return;

            CurrentKillCount++;
            if (CurrentKillCount >= KillRequirement)
            {
                UnsubscribeGameEvents();
                ParentQuest.Progress();
            }
        }

        public override void Reset()
        {
            base.Reset();
            CurrentKillCount = 0;
        }

        public void SubscribeGameEvents()
        {
            _enemyKilledEvent.AddListener(this, OnEnemyKilled);
        }

        public void UnsubscribeGameEvents()
        {
            _enemyKilledEvent.RemoveListener(this, OnEnemyKilled);
        }
    }
}