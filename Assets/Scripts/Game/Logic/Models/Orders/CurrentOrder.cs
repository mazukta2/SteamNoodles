using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.States;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CurrentOrder
    {
        public Recipe[] Recipes => GetRecipies();

        public uint Id => Id;

        private State _state;
        private uint _id;
        private GameState Get() => _state.Get<GameState>(_id);

        public CurrentOrder(State state, uint id)
        {
            _state = state;
            _id = id;
        }

        public CurrentOrder(State state, AvailableOrder order)
        {
            _state = state;
            (_id, _) =_state.Add(new GameState(order.GetPrototype()));
        }

        public bool Have(IIngredientPrototype ingredient)
        {
            return Recipes.Any(x => x.Ingredient == ingredient);
        }

        public bool IsOpen()
        {
            return Recipes.Any(x => x.IsOpen());
        }

        public Recipe[] GetRecipies()
        {
            return _state.GetAllId<Recipe.GameState>()
                .Select(x => new Recipe(_state, x))
                .ToArray();
        }

        public struct GameState : IStateEntity
        {
            public IOrderPrototype Prototype { get; }

            public GameState(IOrderPrototype proto)
            {
                Prototype = proto;
            }
        }
    }
}
