using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Models.Events.GameEvents
{
    public class SchemeAddedToHandEvent : IGameEvent
    {
        public SchemeAddedToHandEvent(ConstructionScheme building) => (Construction) = (building);
        public ConstructionScheme Construction { get; }
    };
}
