using Game.Assets.Scripts.Game.Environment.Creation;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class RemoveGhostCommand : IPresenterCommand
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
