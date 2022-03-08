using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;

namespace Game.Assets.Scripts.Game.Environment
{
    public class Core : Disposable
    {
        public IGameEngine Engine { get; private set; }
        public GameModel Game { get; private set; }

        public Core(IGameEngine gameEngine)
        {
            Engine = gameEngine;
            Game = new GameModel(Engine);

            CoreAccessPoint.SetCore(this);
        }

        protected override void DisposeInner()
        {
            Game.Dispose();
            CoreAccessPoint.ClearCore();
        }
    }
}
