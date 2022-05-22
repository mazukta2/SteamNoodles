using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class CellUnityView : UnityView<PlacementCellPresenter>, ISwitcher<CellPlacementStatus>, ICellView
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _target;
        [SerializeField] GameObject _highlight;
        [SerializeField] GameObject _blocked;
        [SerializeField] GameObject _disabled;

        [SerializeField] AnimatorUnity _animator;
        [SerializeField] PositionUnity _position;

        private CellPlacementStatus _status;

        public CellPlacementStatus Value { get => _status; set => SetStatus(value); }
        public IPosition LocalPosition => _position;
        public ISwitcher<CellPlacementStatus> State => this;

        public IAnimator Animator => _animator;

        private void SetStatus(CellPlacementStatus status)
        {
            _status = status;

            _normal.SetActive(status == CellPlacementStatus.Normal);
            _target.SetActive(status == CellPlacementStatus.IsReadyToPlace);
            _highlight.SetActive(status == CellPlacementStatus.IsAvailableGhostPlace);
            _blocked.SetActive(status == CellPlacementStatus.IsNotAvailableGhostPlace);
            _disabled.SetActive(status == CellPlacementStatus.IsUnderConstruction);
        }

    }

}
