using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PieceSpawnerView : PresenterView<PointPieceSpawnerPresenter>, IPointPieceSpawner
    {
        public IViewContainer Container { get; }
        public IViewPrefab PiecePrefab { get; }

        public PieceSpawnerView(LevelView level) : base(level)
        {
            Container = new ContainerViewMock(level);
            PiecePrefab = new PiecePrefabMock();
        }

        private class PiecePrefabMock : ViewPrefabMock
        {
            public override IView CreateView<T>(LevelView level, ContainerViewMock container)
            {
                return new PieceView(level);
            }
        }
    }
}