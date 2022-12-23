// using System;
// using ChiciStudios.ProjectPhoenix.Enums;
// using ChiciStudios.ProjectPhoenix.Globals;
// using ChiciStudios.ProjectPhoenix.Settings;
// using TMPro;
// using UnityEngine;
//
// namespace ChiciStudios.ProjectPhoenix.UI
// {
//     public class KeyMapper : MonoBehaviour
//     {
//         [SerializeField]
//         private InputMap _inputMap;
//         
//         [SerializeField]
//         private TMP_InputField _leftInput;
//         
//         [SerializeField]
//         private TMP_InputField _rightInput;
//         
//         [SerializeField]
//         private TMP_InputField _upInput;
//         
//         [SerializeField]
//         private TMP_InputField _downInput;
//         
//         [SerializeField]
//         private TMP_InputField _rollInput;
//         
//         [SerializeField]
//         private TMP_InputField _attackInput;
//
//         private void Start()
//         {
//             _leftInput.text = _inputMap.HorizontalAxisKeys.Negative.ToString();
//             _rightInput.text = _inputMap.HorizontalAxisKeys.Positive.ToString();
//             _upInput.text = _inputMap.VerticalAxisKeys.Positive.ToString();
//             _downInput.text = _inputMap.VerticalAxisKeys.Negative.ToString();
//             _rollInput.text = _inputMap.KeyMap[CommandType.Roll].ToString();
//             _attackInput.text = _inputMap.KeyMap[CommandType.Attack].ToString();
//             _leftInput.onEndEdit.AddListener((input) => _inputMap.HorizontalAxisKeys.Negative = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//             _rightInput.onEndEdit.AddListener((input) => _inputMap.HorizontalAxisKeys.Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//             _upInput.onEndEdit.AddListener((input) => _inputMap.VerticalAxisKeys.Positive = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//             _downInput.onEndEdit.AddListener((input) => _inputMap.VerticalAxisKeys.Negative = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//             _rollInput.onEndEdit.AddListener((input) => _inputMap.KeyMap[CommandType.Roll] = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//             _attackInput.onEndEdit.AddListener((input) => _inputMap.KeyMap[CommandType.Attack] = (KeyCode)System.Enum.Parse(typeof(KeyCode), input));
//         }
//     }
// }
