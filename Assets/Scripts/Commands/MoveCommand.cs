using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public class MoveCommand : ICommand
    {
        public Vector2 Distance { get; set; }
        
        public void Execute(ICommandActor target)
        {
            if (target is not IMoveCommandActor moveActor)
            {
                Debug.LogWarning("Cannot execute Move command - target does not implement IMoveCommandActor!");
                return;
            }
            moveActor.Move(Distance);
        }
    }
}