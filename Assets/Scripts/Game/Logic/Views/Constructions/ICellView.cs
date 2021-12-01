using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface ICellView : IView
    {
        void SetState(CellPresenter.CellState state);
        CellPresenter.CellState GetState();
        void SetPosition(Vector2 vector2);
    }
}
