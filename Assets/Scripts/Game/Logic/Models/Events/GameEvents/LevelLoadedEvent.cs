using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class LevelLoadedEvent : IGameEvent
    {
        public LevelLoadedEvent(GameLevel level, ILevelView levelView) => (Model, View) = (level, levelView);
        public GameLevel Model { get; }
        public ILevelView View { get; }
    };
}
