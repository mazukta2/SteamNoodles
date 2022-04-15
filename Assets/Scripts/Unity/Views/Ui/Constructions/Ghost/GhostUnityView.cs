using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostUnityView : UnityView<GhostPresenter>, IGhostView
    {
        [SerializeField] ContainerUnityView _container;

        public ILevelPosition LocalPosition { get; private set; }
        public IViewContainer Container => _container;
        public IRotator Rotator { get; private set; }
        public bool CanPlace { get; set; }

        protected override void PreAwake()
        {
            LocalPosition = new UnityLevelPosition(transform);
            Rotator = new UnityRotator(transform);
        }

    }

}
