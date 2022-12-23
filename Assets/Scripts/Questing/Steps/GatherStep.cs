using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing.Steps
{
    [Serializable]
    public class GatherStep : QuestStep, IGameEventConsumer
    {
        [SerializeField]
        private GameEvent _lootGatheredEvent;
        
        [field: SerializeField]
        public int ItemId { get; set; }

        [field: SerializeField]
        public int GatherRequirement { get; set; }
        
        public int CurrentGatherCount { get; set; }

        public override void Activate()
        {
            base.Activate();
            SubscribeGameEvents();
        }

        private void OnLootGathered(Dictionary<string, object> msg)
        {
            if (!_lootGatheredEvent.TryGetValueFromMessage<int>(msg, "itemId", out var itemId) || itemId != ItemId) return;
            
            if (!_lootGatheredEvent.TryGetValueFromMessage<int>(msg, "amount", out var amount)) return;

            CurrentGatherCount += amount;

            if (CurrentGatherCount >= GatherRequirement)
            {
                UnsubscribeGameEvents();
                ParentQuest.Progress();
            }
        }

        public override void Reset()
        {
            base.Reset();
            CurrentGatherCount = 0;
        }

        public void SubscribeGameEvents()
        {
            _lootGatheredEvent.AddListener(this, OnLootGathered);
        }

        public void UnsubscribeGameEvents()
        {
            _lootGatheredEvent.AddListener(this, OnLootGathered);
        }
    }
}