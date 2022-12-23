using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Items
{
    [CreateAssetMenu(fileName = "QuestItemData", menuName = "ScriptableObjects/Items/QuestItem", order = 1)]
    public class QuestItem : Item
    {
        [field: SerializeField]
        public QuestLookup[] Quests { get; set; }
    }
}