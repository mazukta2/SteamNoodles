using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PlacementFieldUnityView : UnityView<PlacementFieldPresenter>, IPlacementFieldView
    {
        [SerializeField] int _id;
        [SerializeField] PlacementManagerViewUnityView _manager;

        public IPlacementManagerView Manager => _manager;
        public int Id => _id;
    }

}
