using System.Collections.Generic;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Questing
{
    [CreateAssetMenu(fileName = "NewQuestStore", menuName = "ScriptableObjects/Quests/QuestStore", order = 1)]
    public class QuestStore : ScriptableObject
    {
        public List<Quest> Quests { get; set; } = new List<Quest>();
    }
}