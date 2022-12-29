using System;
using System.Linq;
using ChiciStudios.ProjectPhoenix.Enums;
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

        [SerializeField]
        private QuestTabGroup _tabGroup;
        
        [field: SerializeField]
        public Quest[] QuestPool { get; set; }

        private void OnEnable()
        {
            _questLookupContainer.DestroyAllChildren();
            _tabGroup.SelectMain();
        }

        public void PopulateQuestLookup(params QuestType[] types)
        {
            bool firstSelected = false;
            _questLookupContainer.DestroyAllChildren();
            foreach (var quest in QuestPool.Where(q => types.Contains(q.Type)))
            {
                var questNode = Instantiate(_questLookupNodePrefab, _questLookupContainer, false);
                var node = questNode.GetComponent<QuestLookupNode>();
                node.Preview = _preview;
                node.AssignQuest(quest);
                if (!firstSelected)
                {
                    firstSelected = true;
                    _preview.LoadQuest(node);
                }
            }
        }
    }
}
