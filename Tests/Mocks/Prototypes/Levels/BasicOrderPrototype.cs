using Assets.Scripts.Logic.Prototypes.Levels;
using System.Collections.Generic;

namespace Tests.Mocks.Prototypes.Levels
{
    public class BasicOrderPrototype : IOrderPrototype
    {
        public List<IIngredientPrototype> Ingredient { get; set; } = new List<IIngredientPrototype>();
        public IIngredientPrototype[] RequiredIngredients => Ingredient.ToArray();
    }
}
