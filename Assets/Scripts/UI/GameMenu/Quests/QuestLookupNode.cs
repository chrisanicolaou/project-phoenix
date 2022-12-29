using ChiciStudios.ProjectPhoenix.Questing;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ChiciStudios.ProjectPhoenix.UI.GameMenu.Quests
{
    public class QuestLookupNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField]
        private TextMeshProUGUI _title;

        private Quest _quest;

        private bool _hovered;
        
        public QuestPreview Preview { get; set; }

        public void AssignQuest(Quest quest)
        {
            _quest = quest;
            _title.text = _quest.Name;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _hovered = true;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _hovered = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_hovered) Preview.LoadQuest(_quest);
        }
    }
}