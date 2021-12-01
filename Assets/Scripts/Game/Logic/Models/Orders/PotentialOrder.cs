using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class PotentialOrder
    {
        private IOrderSettings _order;
        private Placement _placement;

        public PotentialOrder(Placement placement, IOrderSettings order)
        {
            _order = order;
            _placement = placement;
        }

        public bool CanBeOrder()
        {
            return true;
        }

        public IOrderSettings GetPrototype()
        {
            return _order;
        }

    }
}
