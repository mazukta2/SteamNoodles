using Game.Assets.Scripts.Game.Logic.Controllers;
using Game.Assets.Scripts.Game.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic
{
    public static class GameAccessPoint
    {
        public static GameModel Game { get; private set; }

        public static void SetGame(GameModel game)
        {
            if (Game != null)
                throw new Exception("Game already setted");

            Game = game;
        }

        public static void ClearGame()
        {
            if (Game == null)
                throw new Exception("Game not setted");

            Game = null;
        }
    }
}
