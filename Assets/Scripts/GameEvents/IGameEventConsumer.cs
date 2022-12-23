using System;
using System.Collections.Generic;
using ChiciStudios.ProjectPhoenix.Globals;

namespace ChiciStudios.ProjectPhoenix.GameEvents
{
    // Makes sure that all consumers implement SubscribeEvent and UnsubscribeEvent methods.
    // Helps to protect me from forgetting to unsubscribe events!
    public interface IGameEventConsumer
    {
        public void SubscribeGameEvents();
        public void UnsubscribeGameEvents();
    }
}