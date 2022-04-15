using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class ConstructionUnityView : UnityView<ConstructionPresenter>, IConstructionView
    {
        [SerializeField] ContainerUnityView _container;

        public ILevelPosition LocalPosition => new UnityLevelPosition(transform);
        public IRotator Rotator => new UnityRotator(transform);
        public IViewContainer Container => _container;
    }

}
