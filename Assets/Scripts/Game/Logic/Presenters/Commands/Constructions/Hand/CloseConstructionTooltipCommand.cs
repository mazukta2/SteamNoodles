using Game.Assets.Scripts.Game.Environment.Creation;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class CloseConstructionTooltipCommand : IPresenterCommand
    {
        private IViewContainer _tooltipContainer;

        public CloseConstructionTooltipCommand(IViewContainer tooltipContainer)
        {
            _tooltipContainer = tooltipContainer;
        }

        public void Execute()
        {
            _tooltipContainer.Clear();
        }
    }
}
