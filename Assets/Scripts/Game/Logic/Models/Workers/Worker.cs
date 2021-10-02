using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class Worker
    {
        private GameState _state;

        public Worker(OrderManager orderManager, Placement placement, GameTime gameTime)
        {
            _state = new GameState();

            _state.Orders = orderManager;
            _state.Placement = placement;
            _state.GameTime = gameTime;

            _state.Placement.OnConstructionAdded += (c) => UpdateJob();
            _state.Placement.OnConstructionRemoved += (c) => UpdateJob();
            _state.Orders.OnCurrentOrderChanged += UpdateJob;
        }

        public Job Job { get => _state.Job; set => _state.Job = value; }

        public void UpdateJob()
        {
            if (Job == null)
            {
                if (_state.Orders.CurrentOrder != null)
                {
                    var recipe = _state.Orders.CurrentOrder.Recipes.Where(x => x.IsOpen()).FirstOrDefault();
                    if (recipe != null)
                    {
                        Job = new RecipeJob(_state.Placement, _state.GameTime, recipe);
                        Job.OnStop += HandleJobStop;
                    }
                    else
                    {
                        // all recipies is compited. do nothing
                    }
                }
            }
        }

        private void HandleJobStop()
        {
            if (Job == null)
                throw new Exception("Unknown job is stopped");

            Job.OnStop -= HandleJobStop;
            Job = null;
            UpdateJob();
        }

        public struct GameState : IStateEntity
        {
            public Job Job { get; set; }
            public OrderManager Orders { get; internal set; }
            public Placement Placement { get; internal set; }
            public GameTime GameTime { get; internal set; }
        }

    }
}
