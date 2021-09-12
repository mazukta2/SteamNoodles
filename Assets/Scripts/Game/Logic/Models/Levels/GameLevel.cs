using Assets.Scripts.Game.Logic.Contexts;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Models.Events;

namespace Assets.Scripts.Logic.Models.Levels
{
    public class GameLevel
    {
        public Placement Placement { get; }

        private ILevelContext _context;

        public PlayerHand Hand { get; }
        public History History { get; } = new History();

        public GameLevel(ILevelPrototype prototype, ILevelContext levelContext)
        {
            Hand = new PlayerHand(prototype.StartingHand, _context.CreateHand());
            Placement = new Placement(prototype.Size);

            _context = levelContext;
        }
    }
}
