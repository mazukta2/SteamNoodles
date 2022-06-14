using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostUnityView : UnityView<GhostPresenter>, IGhostView
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] PositionUnity _position;

        public IPosition LocalPosition => _position;
        public IViewContainer Container => _container;
        public IRotator Rotator { get; private set; }
        public bool CanPlace { get; set; }

        protected override void PreAwake()
        {
            Rotator = new UnityRotator(transform);
        }

    }

}
