using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Hand
{
    public class CloseConstructionTooltipCommand : ICommand
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
