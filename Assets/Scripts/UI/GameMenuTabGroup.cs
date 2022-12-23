using System.Linq;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI
{
    public class GameMenuTabGroup : MonoBehaviour
    {
        [SerializeField]
        private GameMenuTab[] _tabs;

        public GameMenuTab SelectedTab { get; set; }

        public void SelectTab(Tab tabType)
        {
            var tab = _tabs.FirstOrDefault(t => t.TabType == tabType);
            if (tab == null) return;
            
            SelectTab(tab);
        }

        public void SelectTab(GameMenuTab tab)
        {
            if (tab == SelectedTab) return;
            foreach (var gameMenuTab in _tabs)
            {
                if (gameMenuTab == tab) continue;
                gameMenuTab.Deselect();
            }
            tab.Select();
            SelectedTab = tab;
        }
    }
}