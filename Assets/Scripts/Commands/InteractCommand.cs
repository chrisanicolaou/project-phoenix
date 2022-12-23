using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public class InteractCommand : ICommand
    {
        public void Execute(ICommandActor target)
        {
            if (target is not IInteractCommandActor interactActor)
            {
                Debug.LogWarning("Cannot execute Interact command - target does not implement IInteractCommandActor!");
                return;
            }
            interactActor.Interact();
        }
    }
}