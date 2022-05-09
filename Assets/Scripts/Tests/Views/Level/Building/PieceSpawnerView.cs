using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PieceSpawnerView : ViewWithPresenter<PointPieceSpawnerPresenter>, IPointPieceSpawner
    {
        public IViewContainer Container { get; }
        public IViewPrefab PiecePrefab { get; }

        public PieceSpawnerView(ILevelView level) : base(level)
        {
            Container = new ContainerViewMock(level);
            PiecePrefab = new PiecePrefabMock();
        }

        private class PiecePrefabMock : ViewPrefabMock
        {
            public override IView CreateView<T>(ILevelView level, ContainerViewMock container)
            {
                return new PieceView(level);
            }
        }

    }
}