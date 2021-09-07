using Assets.Scripts.ViewModels.Buildings;
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

        private PlacementViewModel _model;
        private float _size;
        private Vector2Int _position;

        public void Set(PlacementViewModel model, int x, int y)
        {
            _model = model;
            _size = model.CellSize;
            transform.localPosition = new Vector3(x * _size, y * _size, 1);
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
            /*
            if (_model.Ghost != null)
            {
                var position = _model.GetGhostCell();
                var occupiedPositions = _model.Ghost.GetOccupiedSpace(position);
                var isTarget = occupiedPositions.Any(p => p == _position);
                var requirementsPosition = _model.GetPlaceablePositions(_model.Ghost);

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
            */
            return Mode.Normal;
        }

//        private void OnDrawGizmos()
//        {
//#if UNITY_EDITOR
//                GUIStyle style = new GUIStyle();
//                style.normal.textColor = new Color(222f / 255, 49f / 255, 99f / 255);
//                style.fontSize = (int)20;
//                Handles.Label(transform.position - _size * Vector3.one, $"({_position.x}:{_position.y})", style);
//#endif
//        }

        private enum Mode
        {
            Normal,
            Target,
            Highlight,
            Blocked
        }
    }
}