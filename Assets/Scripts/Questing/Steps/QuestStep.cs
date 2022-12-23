using System;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing.Steps
{
    [Serializable]
    public abstract class QuestStep
    {
        [field: SerializeField]
        public QuestStepType Type { get; set; }
        
        [field: SerializeField]
        public Quest ParentQuest { get; set; }

        public QuestState State { get; set; } = QuestState.Locked;

        public virtual void Activate()
        {
            State = QuestState.Active;
        }

        public virtual void Reset()
        {
            State = QuestState.Locked;
        }
    }
}