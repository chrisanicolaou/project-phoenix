using ChiciStudios.ProjectPhoenix.Questing;
using ChiciStudios.ProjectPhoenix.Utils.Extensions;
using TMPro;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestPreview: MonoBehaviour
    {
        public QuestLookupNode CurrentlySelectedNode { get; set; }
        [SerializeField]
        private TextMeshProUGUI _title;
        
        [SerializeField]
        private TextMeshProUGUI _description;
        
        [SerializeField]
        private TextMeshProUGUI _stepProgress;
        
        [SerializeField]
        private TextMeshProUGUI _reward;

        public void LoadQuest(QuestLookupNode questNode)
        {
            if (CurrentlySelectedNode != null) CurrentlySelectedNode.Deselect();

            CurrentlySelectedNode = questNode;
            _title.text = questNode.Quest.Name;
            _description.text = questNode.Quest.Description;
            _stepProgress.text = questNode.Quest.Steps[questNode.Quest.CurrentStepIndex].ProgressText;
            _reward.text = questNode.Quest.Reward.GoldReward ? $"Reward: {(questNode.Quest.Reward.GoldAmount + "g").ToTMProColor(Color.yellow)}" : "Reward:";
            foreach (var itemReward in questNode.Quest.Reward.Items)
            {
                _reward.text += $"\n{itemReward.Quantity}x {itemReward.Item.Name}".ToTMProColor(Color.magenta);
            }
            questNode.OnSelect();
        }
    }
}