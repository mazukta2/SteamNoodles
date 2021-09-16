using Assets.Scripts.Models.Buildings;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface ICellView : IView
    {
        void SetState(CellViewModel.CellState state);
        CellViewModel.CellState GetState();
        void SetPosition(Vector2 vector2);
    }
}
