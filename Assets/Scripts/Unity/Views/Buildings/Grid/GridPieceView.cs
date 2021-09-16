using Assets.Scripts.Core;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridPieceView : GameMonoBehaviour, ICellView
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _target;
        [SerializeField] GameObject _highlight;
        [SerializeField] GameObject _blocked;

        private CellViewModel.CellState _state;

        public CellViewModel.CellState GetState()
        {
            return _state;
        }

        public void SetPosition(System.Numerics.Vector2 vector2)
        {
            transform.position = new Vector2(vector2.X, vector2.Y);
        }

        public void SetState(CellViewModel.CellState state)
        {
            _state = state;
            UpdateView();
        }

        private void UpdateView()
        {
            _normal.SetActive(_state == CellViewModel.CellState.Normal);
            _target.SetActive(_state == CellViewModel.CellState.IsReadyToPlace);
            _highlight.SetActive(_state == CellViewModel.CellState.IsAvailableGhostPlace);
            _blocked.SetActive(false);
        }
    }
}