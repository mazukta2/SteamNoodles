using Assets.Scripts.Game.Logic.Common.Math;
using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements
{
    public class PlacementViewModel
    {
        public readonly float CellSize = 0.25f;

        public ConstructionGhostViewModel Ghost { get; private set; }

        private Placement _model;

        public PlacementViewModel(Placement model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

        }

        public void SetGhost(ConstructionScheme obj)
        {
            if (Ghost != null) throw new Exception("Ghost already existing");

            Ghost = new ConstructionGhostViewModel(this, obj);
        }

        public void OnClick(Vector2 worldPosition)
        {
            if (Ghost != null)
            {
                var targetPosition = Ghost.GetCellPosition(worldPosition);
                if (!_model.CanPlace(Ghost.Scheme, targetPosition))
                {
                    _model.Place(Ghost.Scheme, targetPosition);
                    Ghost = null;
                }
            }
        }

        //public IPoint GetWorldPosition(IPoint cell)
        //{
        //    return new Vector3(cell.x * CellSize - CellSize / 2, cell.y * CellSize - CellSize / 2);
        //}

        //public Point WorldToCell(Vector2 positon)
        //{
        //    var offset = new Vector3(Size.X * CellSize / 2 - CellSize / 2,
        //        Ghost.Size.y * CellSize / 2 - CellSize / 2);

        //    mouseWorldPosition -= offset;

        //    var mousePosX = Mathf.RoundToInt(mouseWorldPosition.x / CellSize);
        //    var mousePosY = Mathf.RoundToInt(mouseWorldPosition.y / CellSize);

        //    var cell = new Vector2Int(Mathf.CeilToInt(mousePosX), Mathf.CeilToInt(mousePosY));
        //    return cell;
        //}
    }
}
