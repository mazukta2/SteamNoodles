using Assets.Scripts.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridPieceView : ViewMonoBehaviour, ICellView
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _target;
        [SerializeField] GameObject _highlight;
        [SerializeField] GameObject _blocked;


        private CellPresenter.CellState _state;
        public CellPresenter.CellState GetState()
        {
            return _state;
        }

        public void SetPosition(System.Numerics.Vector2 vector2)
        {
            transform.position = new Vector3(vector2.X, 0, vector2.Y);
        }

        public void SetState(CellPresenter.CellState state)
        {
            _state = state;
            UpdateView();
        }

        private void UpdateView()
        {
            _normal.SetActive(_state == CellPresenter.CellState.Normal);
            _target.SetActive(_state == CellPresenter.CellState.IsReadyToPlace);
            _highlight.SetActive(_state == CellPresenter.CellState.IsAvailableGhostPlace);
            _blocked.SetActive(_state == CellPresenter.CellState.IsNotAvailableGhostPlace);
        }
    }
}