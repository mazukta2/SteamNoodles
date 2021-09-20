using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class BasicCellView : TestView, ICellView
    {
        private CellViewModel.CellState _state;

        public Vector2 Position { get; private set; }

        public void SetState(CellViewModel.CellState state)
        {
            _state = state;
        }

        public CellViewModel.CellState GetState()
        {
            return _state;
        }

        public void SetPosition(Vector2 vector2)
        {
            Position = vector2;
        }
    }
}
