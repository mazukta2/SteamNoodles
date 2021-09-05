using Assets.Scripts.Data.Buildings;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Models.Levels
{
    public class GameLevel
    {
        public GameLevel(BuildingsData data)
        {
            Data = data;
            Placement = new Placement(data);
            Hand = new PlayerHand(data);
        }

        protected BuildingsData Data { get; }

        public Placement Placement { get; }
        public PlayerHand Hand { get; }
        public History History { get; } = new History();

        public GameLevel(GameLevel origin)
        {
            Placement = origin.Placement;
            Hand = origin.Hand;
        }
    }
}
