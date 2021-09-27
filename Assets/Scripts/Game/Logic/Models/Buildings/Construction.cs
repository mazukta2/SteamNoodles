using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Assets.Scripts.Models.Buildings
{
    public class Construction
    {
        private Placement _grid;

        public Construction(Placement grid, ConstructionScheme scheme, Point position)
        {
            Scheme = scheme;
            Position = position;
            _grid = grid;
        }

        public Point Position { get; private set; }
        public ConstructionScheme Scheme { get; private set; }

        public bool IsProvide(IIngredientPrototype ingredient)
        {
            return (Scheme.ProvidedIngridient == ingredient);
        }

        public Point[] GetOccupiedScace()
        {
            return Scheme.GetOccupiedSpace(Position);
        }

        public IVisual GetVisual()
        {
            return Scheme.BuildingView;
        }
    }
}
