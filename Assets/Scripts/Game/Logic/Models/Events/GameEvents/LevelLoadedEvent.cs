using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class LevelLoadedEvent : IGameEvent
    {
        public LevelLoadedEvent(GameLevel level) => (Level) = (level);
        public GameLevel Level { get; }
    };
}
