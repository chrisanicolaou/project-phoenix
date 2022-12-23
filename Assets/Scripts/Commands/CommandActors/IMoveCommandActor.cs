using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands.CommandActors
{
    public interface IMoveCommandActor : ICommandActor
    {
        public void Move(Vector2 distance);
    }
}