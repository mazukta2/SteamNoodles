using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Units
{
    public class UnitsManagerUnityView : UnityView<UnitsManagerView>
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] PrototypeUnityView _prototype;

        protected override UnitsManagerView CreateView()
        {
            return new UnitsManagerView(Level, _container.View, _prototype.View);
        }

    }

}
