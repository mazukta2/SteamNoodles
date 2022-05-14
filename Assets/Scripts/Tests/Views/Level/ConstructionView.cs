using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level
{
    public class ConstructionView : ViewWithPresenter<ConstructionPresenter>, IConstructionView
    {
        public IPosition Position { get; set; } = new PositionMock();
        public IRotator Rotator { get; }
        public IViewContainer Container { get; set; }

        public ConstructionView(IViewsCollection level, IViewContainer container, IRotator rotator) : base(level)
        {
            Container = container;
            Rotator = rotator;
        }

    }
}
