using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PiecesSpawnerUnityView : UnityView<PointPieceSpawnerPresenter>, IPointPieceSpawner
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] PrototypeUnityView _prefab;

        IViewContainer IPointPieceSpawner.Container => _container;
        IViewPrefab IPointPieceSpawner.PiecePrefab => _prefab;

        protected override void PreAwake()
        {
        }
    }

}
