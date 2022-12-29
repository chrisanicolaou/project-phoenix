using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestTabGroup : MonoBehaviour
    {
        [SerializeField]
        private QuestTab _mainQuestTab;
        
        [SerializeField]
        private QuestTab _sideQuestTab;
        
        [SerializeField]
        private QuestLookupGenerator _questLookupGenerator;

        public void SelectMain()
        {
            LoadQuests(_mainQuestTab);
        }

        public void LoadQuests(QuestTab tab)
        {
            if (tab == _mainQuestTab)
            {
                _sideQuestTab.Deselect();
                _questLookupGenerator.PopulateQuestLookup(QuestType.Main);
                return;
            }
            
            _mainQuestTab.Deselect();
            _questLookupGenerator.PopulateQuestLookup(QuestType.Side, QuestType.Farm);
        }
    }
}