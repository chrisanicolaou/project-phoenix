using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using ChiciStudios.ProjectPhoenix.Enums;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public class OpenMenuCommand : ICommand
    {
        public Tab Destination { get; set; }

        public OpenMenuCommand(Tab destination)
        {
            Destination = destination;
        }
         public void Execute(ICommandActor target)
        {
            if (target is not IOpenMenuCommandActor menuCommandActor)
            {
                Debug.LogWarning("Cannot execute OpenMenu command - target does not implement IOpenMenuCommandActor!");
                return;
            }
            
            menuCommandActor.OpenMenu(Destination);
        }
    }
}