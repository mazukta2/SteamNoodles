using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.States.Game.Level
{
    public struct OrderState : IStateEntity
    {
        private State _state;
        public uint Id { get; }
        public IOrderPrototype Prototype { get; }

        public OrderState(State state, uint id, IOrderPrototype proto)
        {
            _state = state;
            Id = id;
            Prototype = proto;
        }

        public Recipe[] GetRecipies()
        {
            return _state.GetAll<RecipeState>()
                .Select(x => new Recipe(x))
                .ToArray();
        }
    }
}
