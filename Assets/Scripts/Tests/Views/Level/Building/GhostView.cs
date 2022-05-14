using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class GhostView : ViewWithPresenter<GhostPresenter>, IGhostView
    {
        public IPosition LocalPosition { get; private set; } = new PositionMock();
        public IViewContainer Container { get; private set; }
        public IRotator Rotator { get; }
        public IPointPieceSpawnerView PieceSpawner { get; }

        public GhostView(IViewsCollection level, IViewContainer container, IRotator rotator) : base(level)
        {
            Container = container;
            Rotator = rotator;
            PieceSpawner = new PieceSpawnerView(level);
        }
    }
}
