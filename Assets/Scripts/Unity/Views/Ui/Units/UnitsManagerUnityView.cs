using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Units
{
    public class UnitsManagerUnityView : UnityView<UnitsPresenter>, IUnitsManagerView
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] PrototypeUnityView _prototype;

        public IViewContainer Container => _container;
        public IViewPrefab UnitPrototype => _prototype;
    }

}
