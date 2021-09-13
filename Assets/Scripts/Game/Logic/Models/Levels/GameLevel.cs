using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using System;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }

        public PlayerHand Hand { get; }

        public GameLevel(ILevelPrototype prototype)
        {
            if (prototype == null) throw new ArgumentNullException(nameof(prototype));

            Hand = new PlayerHand(prototype.StartingHand);
            Placement = new Placement(prototype.Size);
        }
    }
}
