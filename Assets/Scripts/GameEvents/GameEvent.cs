using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.GameEvents
{
    [CreateAssetMenu(fileName = "NewGameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
    public class GameEvent : ScriptableObject
    {
        private Action<Dictionary<string, object>> _event;

        public void AddListener([NotNull] IGameEventConsumer consumer, Action<Dictionary<string, object>> listener)
        {
            _event += listener;
        }
        
        public void RemoveListener([NotNull] IGameEventConsumer consumer, Action<Dictionary<string, object>> listener)
        {
            if (_event.GetInvocationList().Contains(listener))
            {
                _event -= listener;
            }
        }

        public void Fire(Dictionary<string, object> message)
        {
            _event?.Invoke(message);
        }

        public bool TryGetValueFromMessage<T>(Dictionary<string, object> msg, string key, out T value)
        {
            if (!msg.TryGetValue(key, out object objValue))
            {
                Debug.LogError(
                    $"No {key} key added to message. Make sure triggers to this event include a \"{key}\" key.");
                value = default;
                return false;
            }

            try
            {
                value = (T)objValue;
            }
            catch (InvalidCastException e)
            {
                Debug.LogError($"{key} is not of type {typeof(T)}. Key value: {objValue}. Exception:");
                Debug.LogException(e);
                value = default;
                return false;
            }

            return true;
        }
    }
}