using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface ICellView : IView
    {
        void SetState(CellPresenter.CellState state);
        CellPresenter.CellState GetState();
        void SetPosition(Vector2 vector2);
    }
}
