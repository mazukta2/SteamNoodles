using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostManagerUnityView : UnityView<GhostManagerView>
    {
        public ContainerUnityView Container;
        public PrototypeUnityView Prototype;

        protected override GhostManagerView CreateView()
        {
            return new GhostManagerView(Level, Container, Prototype);
        }

    }

}
