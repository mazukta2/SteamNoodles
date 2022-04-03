using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Engine.Controls
{
    public class KeyCommand
    {
        public event Action OnTap = delegate { };

        private GameKeys _button;

        public KeyCommand(GameKeys button)
        {
            _button = button;
        }

        public void Tap()
        {
            OnTap();
        }
    }

    public enum GameKeys
    {
        RotateLeft,
        RotateRight,
    }
}
