using Assets.Scripts.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Mocks.Prototypes.Levels
{
    public class TestRecipePrototype : IRecipePrototype
    {
        public IIngredientPrototype Ingredient { get; } 

        public TestRecipePrototype(IIngredientPrototype ingredient)
        {
            Ingredient = ingredient;
        }

        public int Count { get; set; }
    }
}
