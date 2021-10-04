using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public class RecipeJob : Job
    {
        private GameState _state;

        public RecipeJob(Placement placement, GameTime time, Recipe recipe)
        {
            _state = new GameState();
            _state.Placement = placement;
            _state.GameTime = time;
            _state.StartTime = time.Time;
            _state.Recipe = recipe;
            _state.Construction = placement.Constructions.First(x => x.IsProvide(recipe));
            if (_state.Construction == null) throw new Exception("Not construction to resolve the recipe");

            _state.Recipe.OnComplited += OnRecipeComplited;
        }

        protected override void StopInner()
        {
            _state.Recipe.OnComplited -= OnRecipeComplited;
        }

        public override void OnTime()
        {
            while (GetNextHitTime() <= _state.GameTime.Time)
            {
                HitRecipe();
                if (IsStopped)
                    break;
            }
        }

        private float GetNextHitTime()
        {
            return _state.StartTime + _state.Construction.WorkTime;
        }

        private void HitRecipe()
        {
            _state.StartTime += _state.Construction.WorkTime;
            _state.Recipe.Progress(_state.Construction.WorkProgressPerHit);
        }

        private void OnRecipeComplited()
        {
            Stop();
        }

        public struct GameState : IStateEntity
        {
            public Recipe Recipe { get; set; }
            public Construction Construction { get; set; }
            public Placement Placement { get; set; }
            public GameTime GameTime { get; set; }
            public float StartTime { get; set; }
        }
    }
}
