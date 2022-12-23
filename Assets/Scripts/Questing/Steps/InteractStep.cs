using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing.Steps
{
    [Serializable]
    public class InteractStep : QuestStep, IGameEventConsumer
    {
        [SerializeField]
        private GameEvent _npcInteractionCompleteEvent;
        [field: SerializeField]
        public int NpcId { get; set; }

        public override void Activate()
        {
            base.Activate();
            SubscribeGameEvents();
        }

        public virtual void OnInteractionComplete(Dictionary<string, object> msg)
        {
            if (!_npcInteractionCompleteEvent.TryGetValueFromMessage<int>(msg, "npcId", out var npcId) || npcId != NpcId) return;
            
            UnsubscribeGameEvents();
            ParentQuest.Progress();
        }

        public void SubscribeGameEvents()
        {
            _npcInteractionCompleteEvent.AddListener(this, OnInteractionComplete);
        }

        public void UnsubscribeGameEvents()
        {
            _npcInteractionCompleteEvent.RemoveListener(this, OnInteractionComplete);
        }
    }
}