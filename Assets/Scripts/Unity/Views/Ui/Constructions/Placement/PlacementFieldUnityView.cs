using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PlacementFieldUnityView : UnityView<PlacementFieldView>
    {
        [SerializeField] int _id;
        [SerializeField] PlacementManagerViewUnityView _manager;

        protected override PlacementFieldView CreateView()
        {
            return new PlacementFieldView(Level, _manager.View, _id);
        }

    }

}
