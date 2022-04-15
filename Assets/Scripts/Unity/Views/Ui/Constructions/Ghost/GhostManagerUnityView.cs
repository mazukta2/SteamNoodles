using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostManagerUnityView : UnityView<GhostManagerPresenter>, IGhostManagerView
    {
        public ContainerUnityView Container;
        public PrototypeUnityView Prototype;

        public IViewPrefab GhostPrototype => Prototype;
        IViewContainer IGhostManagerView.Container => Container;
    }

}
