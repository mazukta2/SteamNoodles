using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class CellUnityView : UnityView<CellView>
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _target;
        [SerializeField] GameObject _highlight;
        [SerializeField] GameObject _blocked;
        protected override CellView CreateView()
        {
            return new CellView(Level, new UnityLevelPosition(transform));
        }

        protected override void AfterAwake()
        {
            View.OnUpdate += View_OnUpdate;
        }

        protected override void OnDisposeView(CellView view)
        {
            view.OnUpdate -= View_OnUpdate;
        }

        private void View_OnUpdate()
        {
            _normal.SetActive(View.State == CellPlacementStatus.Normal);
            _target.SetActive(View.State == CellPlacementStatus.IsReadyToPlace);
            _highlight.SetActive(View.State == CellPlacementStatus.IsAvailableGhostPlace);
            _blocked.SetActive(View.State == CellPlacementStatus.IsNotAvailableGhostPlace);
        }

    }

}
