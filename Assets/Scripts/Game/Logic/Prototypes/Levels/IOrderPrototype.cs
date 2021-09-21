using Assets.Scripts.Game.Logic.Common.Math;
using System;
using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IOrderPrototype
    {
        public IIngredientPrototype[] RequiredIngredients { get; }
    }
}
