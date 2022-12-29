using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.GameEvents;
using ChiciStudios.ProjectPhoenix.Globals;
using ChiciStudios.ProjectPhoenix.Questing.Steps;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing
{
    [CreateAssetMenu(fileName = "NewQuest", menuName = "ScriptableObjects/Quests/Quest", order = 1)]
    public class Quest : ScriptableObject
    {
        [SerializeField]
        private QuestStore _activeQuestsStore;
        
        [SerializeField]
        private GameEvent _questUnlockedEvent;
        
        [SerializeField]
        private GameEvent _questActivatedEvent;
        
        [field: SerializeField]
        public int Id { get; set; }

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        public string Description { get; set; }

        [field: SerializeField]
        public QuestType Type { get; set; }

        [field: SerializeReference]
        public List<QuestStep> Steps { get; set; }

        [field: SerializeField]
        public QuestState State { get; set; } = QuestState.Unlocked;

        public int CurrentStepIndex { get; set; } = 0;

        [field: SerializeField]
        public Quest[] UnlockOnCompletion { get; set; }
        
        [field: SerializeField]
        public QuestReward Reward { get; set; }

        public void Unlock()
        {
            State = QuestState.Unlocked;
            Steps[0].State = QuestState.Unlocked;
            _questUnlockedEvent.Fire(new Dictionary<string, object>
            {
                { "questId", Id }
            });
        }

        public void Activate()
        {
            State = QuestState.Active;
            Steps[0].Activate();
            _activeQuestsStore.Quests.Add(this);
            _questActivatedEvent.Fire(new Dictionary<string, object>
            {
                { "questId", Id }
            });
        }

        public void Progress()
        {
            Steps[CurrentStepIndex].State = QuestState.Complete;

            if (CurrentStepIndex == Steps.Count - 1)
            {
                Complete();
                return;
            }
            
            CurrentStepIndex++;
            Steps[CurrentStepIndex].Activate();
        }

        private void Complete()
        {
            _activeQuestsStore.Quests.Remove(this);
            foreach (var quest in UnlockOnCompletion)
            {
                quest.Unlock();
            }

            if (Type != QuestType.Farm)
            {
                State = QuestState.Complete;
            }
            else
            {
                State = QuestState.Unlocked;
                CurrentStepIndex = 0;
                foreach (var questStep in Steps)
                {
                    questStep.Reset();
                }
            }
        }
    }
}