using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public class RollCommand : ICommand
    {
        public void Execute(ICommandActor target)
        {
            if (target is not IRollCommandActor rollActor)
            {
                Debug.LogWarning("Cannot execute Roll command - target does not implement IRollCommandActor!");
                return;
            }
            rollActor.Roll();
        }
    }
}