using System;
using ChiciStudios.ProjectPhoenix.Commands;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Utils
{
    [Serializable] public class Keymap : SerializableDictionary<CommandType, KeyCode> { }
}