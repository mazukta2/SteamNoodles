using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public class GameTime
    {
        public event Action OnTimeChanged = delegate { };

        private GameState _state;

        public GameTime()
        {
            _state = new GameState();
        }

        public float Time => _state.Time;

        public void MoveTime(float time)
        {
            _state.Time += time;
            OnTimeChanged();
        }

        private class GameState : IStateEntity
        {
            public float Time { get; set; }
        }
    }
}
