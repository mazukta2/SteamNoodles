using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class ConstructionView : PresenterView<ConstructionPresenter>
    {
        public ILevelPosition LocalPosition { get; set; }
        public IRotator Rotator { get; }
        public IViewContainer Container { get; set; }

        public ConstructionView(ILevel level, IViewContainer container, ILevelPosition position, IRotator rotator) : base(level)
        {
            LocalPosition = position;
            Container = container;
            Rotator = rotator;
        }

    }
}
