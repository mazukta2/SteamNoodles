using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class RemoveGhostCommand : ICommand
    {
        private IViewContainer _container;

        public RemoveGhostCommand(IViewContainer container)
        {
            _container = container;
        }

        public void Execute()
        {
            _container.Clear();
        }
    }
}
