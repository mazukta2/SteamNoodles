using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Views.Events;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridView : GameMonoBehaviour
    {
        [SerializeField] PrototypeLink _gridPiece;

        public BuildingViewModel ViewModel { get; private set; }

        private HistoryReader _historyReader;

        public void Set(BuildingViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            ViewModel = viewModel;
            RecreateGrid();

            _historyReader = new HistoryReader(ViewModel.GetGrid().History);
            _historyReader.Subscribe<BuildingsGrid.BuildingAddedEvent>(AddBuilingHandle).Update();
        }

        protected void Update()
        {
            _historyReader.Update();
        }

        private void RecreateGrid()
        {
            _gridPiece.DestroySpawned();
            var rect = ViewModel.GetGrid().GetRect();
            for (int x = rect.x; x < rect.width; x++)
            {
                for (int y = rect.y; y < rect.height; y++)
                {
                    _gridPiece.Create<GridPieceView>(v => v.Set(ViewModel, x, y));
                }
            }
        }


        private void AddBuilingHandle(BuildingsGrid.BuildingAddedEvent obj)
        {
            var view = GameObject.Instantiate(obj.Building.Scheme.View, transform);
            view.transform.position = obj.Building.GetWorldPosition();
        }

    }
}