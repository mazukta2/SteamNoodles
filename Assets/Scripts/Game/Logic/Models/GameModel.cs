using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Session;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameModel : Disposable
    {
        public GameModel()
        {
            IGameSession.Default = new GameSession();
        }

        protected override void DisposeInner()
        {
            IGameSession.Default.Dispose();
            IGameSession.Default = null;
        }
    }

}
