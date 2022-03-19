using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class ConstructionModelUnityView : UnityView<ConstructionModelView>
    {
        protected override ConstructionModelView CreateView()
        {
            return new ConstructionModelView(Level);
        }

    }

}
