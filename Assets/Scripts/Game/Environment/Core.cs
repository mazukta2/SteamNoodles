using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Game.Environment
{
    public class Core : Disposable
    {
        public GameModel Game { get; private set; }

        public Core()
        {
            Game = new GameModel();

            IGameKeysManager.Default = new GameKeysManager();
        }

        protected override void DisposeInner()
        {
            IGameKeysManager.Default = null;
            Game.Dispose();
        }
    }
}
