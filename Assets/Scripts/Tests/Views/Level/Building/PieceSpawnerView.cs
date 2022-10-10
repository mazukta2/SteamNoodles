using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PieceSpawnerView : ViewWithPresenter<PointPieceSpawnerPresenter>, IPointPieceSpawnerView
    {
        public IViewContainer Container { get; }
        public IViewPrefab PiecePrefab { get; }

        public PieceSpawnerView(IViews level) : base(level)
        {
            Container = new ContainerViewMock(level);
            PiecePrefab = new PiecePrefabMock();
        }

        private class PiecePrefabMock : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                new PieceView(collection);
            }
        }

    }
}