using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs
{
    public class RecipeJob : Job
    {
        public uint Id { get; private set; }
        private State _state;

        private GameState Get() => _state.Get<GameState>(Id);

        public RecipeJob(State state, uint id)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            _state = state;
            Id = id;

            //if (!recipe.IsOpen()) throw new Exception("Recipe is closed");
            //if (recipe.GetConstruction() == null) throw new Exception("Not construction to resolve the recipe");

            //_recipe = recipe;
            //_work = work;
            //_construction = _recipe.GetConstruction();
        }

        public RecipeJob(State state, Recipe recipe)
        {
            _state = state;
            (Id, _) = _state.Add(new GameState(recipe.Id));
        }

        //public override void Start()
        //{
        //    //_updater = _time.MakeUpdater(_construction.WorkTime);
        //    //_updater.OnUpdate += HitRecipe;
        //}

        public override void Stop()
        {
            //if (_updater != null)
            //{
            //    _updater.OnUpdate -= HitRecipe;
            //    _updater.Destroy();
            //}
        }

        private void HitRecipe()
        {
            //_recipe.Progress(_construction.WorkProgressPerHit);
        }

        public struct GameState : IStateEntity
        {
            public uint Recipe { get; }

            public GameState(uint recipe)
            {
                Recipe = recipe;
            }
        }
    }
}
