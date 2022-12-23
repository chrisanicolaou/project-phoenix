using ChiciStudios.ProjectPhoenix.Commands.CommandActors;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public interface ICommand
    {
        public void Execute(ICommandActor target);
    }
}