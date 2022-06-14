using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class ConstructionUnityView : UnityView<ConstructionPresenter>, IConstructionView
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] PositionUnity _position;
        [SerializeField] ContainerUnityView _effectsContainer;
        [SerializeField] PrototypeUnityView _explosionPrototype;

        public IPosition Position => _position;
        public IRotator Rotator { get; private set; }
        public IViewContainer Container => _container;
        public IViewContainer EffectsContainer => _effectsContainer;
        public IViewPrefab ExplosionPrototype => _explosionPrototype;

        protected override void PreAwake()
        {
            Rotator = new UnityRotator(transform);
        }
    }

}
