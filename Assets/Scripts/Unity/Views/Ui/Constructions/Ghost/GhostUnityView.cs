using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostUnityView : UnityView<GhostView>
    {
        [SerializeField] ContainerUnityView _container;

        protected override GhostView CreateView()
        {
            return new GhostView(Level, _container, new UnityLevelPosition(transform), new UnityRotator(transform));
        }

    }

}
