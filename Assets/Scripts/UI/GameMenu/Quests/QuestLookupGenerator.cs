using System;
using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.Utils;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestLookupGenerator : MonoBehaviour
    {
        [SerializeField]
        private GameObject _questLookupNodePrefab;

        [SerializeField]
        private Transform _questLookupContainer;

        [SerializeField]
        private QuestPreview _preview;
        
        [field: SerializeField]
        public Quest[] QuestPool { get; set; }

        private void OnEnable()
        {
            _questLookupContainer.DestroyAllChildren();
            PopulateQuestLookup();
        }

        private void PopulateQuestLookup()
        {
            foreach (var quest in QuestPool)
            {
                var questNode = Instantiate(_questLookupNodePrefab, _questLookupContainer, false);
                var node = questNode.GetComponent<QuestLookupNode>();
                node.Preview = _preview;
                node.AssignQuest(quest);
            }
        }
    }
}
