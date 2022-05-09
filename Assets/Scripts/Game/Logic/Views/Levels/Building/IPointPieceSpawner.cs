using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IPointPieceSpawner : IViewWithDefaultPresenter, IViewWithGenericPresenter<PointPieceSpawnerPresenter>
    {
        IViewContainer Container { get; }
        IViewPrefab PiecePrefab { get; }
        void IViewWithDefaultPresenter.Init()
        {
            Presenter = new PointPieceSpawnerPresenter(this);
        }
    }
}