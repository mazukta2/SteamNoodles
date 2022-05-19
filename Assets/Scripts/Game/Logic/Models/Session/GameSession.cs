using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Session
{
    public class GameSession : Disposable, IGameSession
    {

        public GameSession()
        {
        }

        protected override void DisposeInner()
        {
        }
    }
}