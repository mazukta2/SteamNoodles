using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Assets.Scripts.Models.Buildings
{
    public class Construction
    {
        private Placement _grid;
        private IConstructionPrototype _proto;

        public Construction(Placement grid, IConstructionPrototype proto, Point position)
        {
            Scheme = new ConstructionScheme(proto);
            Position = position;
            _grid = grid;
            _proto = proto;
        }

        public Point Position { get; private set; }
        public ConstructionScheme Scheme { get; private set; }
        public TimeSpan WorkTime => _proto.WorkTime;
        public float WorkProgressPerHit => _proto.WorkProgressPerHit;

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
