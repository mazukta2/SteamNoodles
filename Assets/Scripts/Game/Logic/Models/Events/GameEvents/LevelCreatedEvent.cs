using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class LevelCreatedEvent : IGameEvent
    {
        public LevelCreatedEvent(GameLevel level) => (Level) = (level);
        public GameLevel Level { get; }
    };
}
