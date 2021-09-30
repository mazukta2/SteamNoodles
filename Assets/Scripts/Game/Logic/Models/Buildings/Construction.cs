using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.States;
using Game.Assets.Scripts.Game.Logic.States.Game.Level;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Assets.Scripts.Models.Buildings
{
    public class Construction
    {
        public uint Id => _state.Get().Id;
        public ConstructionScheme Scheme => new ConstructionScheme(_state.Get().Prototype);
        public Point Position => _state.Get().Position;
        public TimeSpan WorkTime => _state.Get().Prototype.WorkTime;
        public float WorkProgressPerHit => _state.Get().Prototype.WorkProgressPerHit;

        private StateLink<ConstructionState> _state;

        public Construction(StateLink<ConstructionState> state)
        {
            _state = state;
        }

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
