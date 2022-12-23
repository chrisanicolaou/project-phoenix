using ChiciStudios.ProjectPhoenix.Enums;

namespace ChiciStudios.ProjectPhoenix.Commands.CommandActors
{
    public interface IOpenMenuCommandActor : ICommandActor
    {
        public void OpenMenu(Tab destination);
    }
}