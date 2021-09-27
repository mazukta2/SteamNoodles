using Assets.Scripts.Logic.Prototypes.Levels;
using System.Collections.Generic;

namespace Tests.Mocks.Prototypes.Levels
{
    public class TestOrderPrototype : IOrderPrototype
    {
        public IRecipePrototype[] Recipes => _recipes.ToArray();

        private List<IRecipePrototype> _recipes { get; set; } = new List<IRecipePrototype>();

        public void Add(IRecipePrototype recipe)
        {
            _recipes.Add(recipe);
        }
    }
}
