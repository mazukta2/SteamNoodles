using Assets.Scripts.Views.Events;
using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridPieceView : MonoBehaviour
    {
        [SerializeField] GameObject _normal;
        [SerializeField] GameObject _target;
        [SerializeField] GameObject _highlight;
        [SerializeField] GameObject _blocked;

        private BuildingViewModel _model;
        private Vector2Int _position;

        public void Set(BuildingViewModel model, int x, int y)
        {
            _model = model;
            var size = model.GetGrid().CellSize;
            transform.localPosition = new Vector3(x * size, y * size, 1);
            _position = new Vector2Int(x, y);
        }

        protected void Update()
        {
            var mode = GetMode();
            _normal.SetActive(mode == Mode.Normal);
            _target.SetActive(mode == Mode.Target);
            _highlight.SetActive(mode == Mode.Highlight);
            _blocked.SetActive(mode == Mode.Blocked);
        }

        private Mode GetMode()
        {
            if (_model.Ghost != null)
            {
                var position = _model.GetGhostCell();
                var occupiedPositions = _model.Ghost.GetOccupiedSpace(position);
                var isTarget = occupiedPositions.Any(p => p == _position);
                var requirementsPosition = _model.Ghost.GetPlaceablePositions(_model.GetGrid());

                if (!isTarget)
                {
                    var isRequirement = requirementsPosition.Any(p => p == _position);
                    if (isRequirement)
                        return Mode.Highlight;
                    else
                        return Mode.Normal;
                }
                else
                {
                    var isRequirement = requirementsPosition.Any(p => p == _position);
                    if (isRequirement)
                        return Mode.Target;
                    else
                        return Mode.Blocked;
                }
            }
            return Mode.Normal;
        }

        private enum Mode
        {
            Normal,
            Target,
            Highlight,
            Blocked
        }
    }
}