using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridView : GameMonoBehaviour
    {
        [SerializeField] PrototypeLink _gridPiece;

        //private PlacementViewModel _placement;
        //private HistoryReader _historyReader;

        //public void Set(PlacementViewModel placement)
        //{
        //    if (placement == null) throw new ArgumentNullException(nameof(placement));
        //    _placement = placement;
        //    RecreateGrid();

        //    _historyReader = new HistoryReader(_placement.History);
        //    _historyReader.Subscribe<PlacementViewModel.BuildingAddedEvent>(AddBuilingHandle).Update();
        //}

        //protected void Update()
        //{
        //    _historyReader.Update();
        //}

        //private void RecreateGrid()
        //{
        //    _gridPiece.DestroySpawned();
        //    var rect = _placement.Rect;
        //    for (int x = rect.x; x < rect.x + rect.width; x++)
        //    {
        //        for (int y = rect.y; y < rect.y + rect.height; y++)
        //        {
        //            _gridPiece.Create<GridPieceView>(v => v.Set(_placement, x, y));
        //        }
        //    }
        //}


        //private void AddBuilingHandle(PlacementViewModel.BuildingAddedEvent obj)
        //{
        //    var view = GameObject.Instantiate(obj.Building.View, transform);
        //    view.transform.position = obj.Building.GetWorldPosition();
        //}

    }
}