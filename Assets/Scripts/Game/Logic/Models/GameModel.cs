using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Controllers;
using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public class GameModel : Disposable
    {
        public event Action OnSessionCreated = delegate { };
        public GameSession Session { get; private set; }
        public ISettingsController Settings => _controller.Settings;

        private readonly IGameController _controller;

        public GameModel(IGameController controller)
        {
            _controller = controller;
            GameAccessPoint.SetGame(this);
        }

        protected override void DisposeInner()
        {
            Session?.Dispose();
            GameAccessPoint.ClearGame();
        }

        public void CreateSession()
        {
            if (Session != null) throw new Exception("Need to finish previous session before loading new one");
            Session = new GameSession(_controller.Levels);
            _controller.SetTimeMover(Session.Time.MoveTime);
            OnSessionCreated();
        }

    }

}
