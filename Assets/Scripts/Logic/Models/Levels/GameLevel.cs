using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;

namespace Assets.Scripts.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }
        public PlayerHand Hand { get; }
        public History History { get; } = new History();

        public GameLevel(GameLevel origin)
        {
            Placement = origin.Placement;
            Hand = origin.Hand;
        }

        public GameLevel()
        {
        }
    }
}
