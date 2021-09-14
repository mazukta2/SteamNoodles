using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.Models.Events.GameEvents
{
    public class ConstrcutionAddedEvent : IGameEvent
    {
        public ConstrcutionAddedEvent(Construction building)
        {
            Construction = building;
        }

        public Construction Construction { get; set; }
    }
}
