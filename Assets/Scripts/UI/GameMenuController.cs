using System;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Inputs;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.UI
{
    public class GameMenuController : MonoBehaviour, IOpenMenuCommandActor
    {
        [SerializeField]
        private GameObject _gameMenu;

        [SerializeField]
        private InputHandler _input;

        [SerializeField]
        private GameMenuTabGroup _tabGroup;

        private void Update()
        {
            var command = _input.HandleInput();
            
            if (command is OpenMenuCommand openMenuCommand)
            {
                command.Execute(this);
            }
        }

        public void OpenMenu(Tab destination)
        {
            if (_tabGroup.SelectedTab != null && destination == _tabGroup.SelectedTab.TabType && _gameMenu.activeSelf)
            {
                CloseMenu();
                return;
            }
            _gameMenu.SetActive(true);
            _tabGroup.SelectTab(destination);
        }

        private void CloseMenu()
        {
            _gameMenu.SetActive(false);
        }
    }
}