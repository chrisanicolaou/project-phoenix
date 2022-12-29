using ChiciStudios.ProjectPhoenix.Questing;
using TMPro;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestPreview: MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _title;
        
        [SerializeField]
        private TextMeshProUGUI _description;
        
        [SerializeField]
        private TextMeshProUGUI _stepProgress;
        
        [SerializeField]
        private TextMeshProUGUI _reward;

        public void LoadQuest(Quest quest)
        {
            _title.text = quest.Name;
            _description.text = quest.Description;
            _stepProgress.text = "<color=\"red\">Not implemented!";
            _reward.text = "<color=\"red\">Not implemented!";
        }
    }
}