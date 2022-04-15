using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Tests.Environment.Views.Level
{
    public class ConstructionView : PresenterView<ConstructionPresenter>, IConstructionView
    {
        public ILevelPosition LocalPosition { get; set; }
        public IRotator Rotator { get; }
        public IViewContainer Container { get; set; }

        public ConstructionView(LevelView level, IViewContainer container, ILevelPosition position, IRotator rotator) : base(level)
        {
            LocalPosition = position;
            Container = container;
            Rotator = rotator;
        }

    }
}
