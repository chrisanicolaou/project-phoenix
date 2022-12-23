using ChiciStudios.ProjectPhoenix.NPC.Dialog;
using ChiciStudios.ProjectPhoenix.Questing;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.NPC
{
    [CreateAssetMenu(fileName = "NpcData", menuName = "ScriptableObjects/Npc/NpcData", order = 1)]
    public class NpcData : ScriptableObject
    {
        [field: SerializeField]
        public int Id { get; set; }
        
        [field: SerializeField]
        public string Name { get; set; }
        
        [field: SerializeField]
        public Sprite Sprite { get; set; }
        
        [field: SerializeField]
        public Sprite DialogSprite { get; set; }
        
        [field: SerializeField]
        public DefaultDialog DefaultDialog { get; set; }

        [field: SerializeField]
        public QuestActivation[] QuestsToActivate { get; set; }
        
        [field: SerializeField]
        public SubscribedQuests[] SubscribedQuests { get; set; }
    }
}