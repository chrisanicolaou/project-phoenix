using System;
using System.Collections.Generic;
using System.Diagnostics;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Enums;
using ChiciStudios.ProjectPhoenix.Settings;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace ChiciStudios.ProjectPhoenix.Inputs
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private InputMap _inputMap;
        
        private readonly ICommand _attackCommand = new AttackCommand();
        private readonly ICommand _rollCommand = new RollCommand();
        private readonly ICommand _interactCommand = new InteractCommand();

        public ICommand HandleInput()
        {
            if (!Input.anyKeyDown) return null;
            
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.Attack]))
            {
                return _attackCommand;
            }
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.Roll]))
            {
                return _rollCommand;
            }
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.Interact]))
            {
                return _interactCommand;
            }
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.OpenQuests]))
            {
                return new OpenMenuCommand(Tab.Quest);
            }
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.OpenInventory]))
            {
                return new OpenMenuCommand(Tab.Inventory);
            }
            if (Input.GetKeyDown(_inputMap.KeyMap[CommandType.OpenMap]))
            {
                return new OpenMenuCommand(Tab.Map);
            }
            
            return null;
        }

        public Vector2 GetAxisRaw()
        {
            var dir = Vector2.zero;
            if (Input.GetKey(_inputMap.HorizontalAxisKeys.Positive))
            {
                dir.x += 1;
            }
            if (Input.GetKey(_inputMap.HorizontalAxisKeys.Negative))
            {
                dir.x -= 1;
            }
            if (Input.GetKey(_inputMap.VerticalAxisKeys.Positive))
            {
                dir.y += 1;
            }
            if (Input.GetKey(_inputMap.VerticalAxisKeys.Negative))
            {
                dir.y -= 1;
            }
        
            return dir;
        }
    }
}