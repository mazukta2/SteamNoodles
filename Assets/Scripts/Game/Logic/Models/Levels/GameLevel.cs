using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }
        public PlayerHand Hand { get; }
        public History History { get; } = new History();

        public GameLevel(ILevelPrototype prototype)
        {
            Hand = new PlayerHand(prototype.StartingHand);
            Placement = new Placement(prototype.Size);
        }
    }
}
