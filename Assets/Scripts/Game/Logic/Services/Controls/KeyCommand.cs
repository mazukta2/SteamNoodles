using System;

namespace Game.Assets.Scripts.Game.Logic.Services.Controls
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
        Exit,
    }
}
