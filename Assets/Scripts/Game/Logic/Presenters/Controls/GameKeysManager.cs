﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Controls
{
    public class GameKeysManager : IGameKeysManager
    {
        private Dictionary<GameKeys, KeyCommand> _keys = new Dictionary<GameKeys, KeyCommand>();

        public GameKeysManager()
        {
            foreach (GameKeys key in Enum.GetValues(typeof(GameKeys)))
            {
                _keys.Add(key, new KeyCommand(key));
            }
        }
        public KeyCommand GetKey(GameKeys key)
        {
            return _keys[key];
        }

        public void TapKey(GameKeys key)
        {
            GetKey(key).Tap();
        }
    }
}
