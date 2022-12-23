using ChiciStudios.ProjectPhoenix.Commands.CommandActors;
using UnityEngine;

namespace ChiciStudios.ProjectPhoenix.Commands
{
    public class AttackCommand : ICommand
    {
        public void Execute(ICommandActor target)
        {
            if (target is not IAttackCommandActor attackActor)
            {
                Debug.LogWarning("Cannot execute Attack command - target does not implement IAttackCommandActor!");
                return;
            }
            attackActor.Attack();
        }
    }
}