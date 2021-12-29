using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System.Numerics;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicCellView : TestView, ICellView
    {
        private CellPresenter.CellState _state;

        public Vector2 Position { get; private set; }

        public void SetState(CellPresenter.CellState state)
        {
            _state = state;
        }

        public CellPresenter.CellState GetState()
        {
            return _state;
        }

        public void SetPosition(Vector2 vector2)
        {
            Position = vector2;
        }

        protected override void DisposeInner()
        {
        }
    }
}
