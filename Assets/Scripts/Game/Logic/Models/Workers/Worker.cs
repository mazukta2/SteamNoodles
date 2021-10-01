using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Workers.Jobs;
using Game.Assets.Scripts.Game.Logic.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class Worker
    {
        public uint Id { get; private set; }

        private State _state;
        private OrderManager _orderManager;

        private GameState Get() => _state.Get<GameState>(Id);


        public Worker(State state, uint id, OrderManager orderManager)
        {
            _state = state;
            _orderManager = orderManager;
            Id = id;
            _state.Subscribe<Construction.GameState>(HandleUpdateConstruction, States.Events.StateEventType.Add);
            _state.Subscribe<Construction.GameState>(HandleUpdateConstruction, States.Events.StateEventType.Remove);
            _state.Subscribe<CurrentOrder.GameState>(HandleUpdateOrder, States.Events.StateEventType.Add);
            _state.Subscribe<CurrentOrder.GameState>(HandleUpdateOrder, States.Events.StateEventType.Remove);
        }

        public Worker(State state, OrderManager orderManager)
        {
            _state = state;
            _orderManager = orderManager;
            (Id, _) = _state.Add(new GameState());
            _state.Subscribe<Construction.GameState>(HandleUpdateConstruction, States.Events.StateEventType.Add);
            _state.Subscribe<Construction.GameState>(HandleUpdateConstruction, States.Events.StateEventType.Remove);
            _state.Subscribe<CurrentOrder.GameState>(HandleUpdateOrder, States.Events.StateEventType.Add);
            _state.Subscribe<CurrentOrder.GameState>(HandleUpdateOrder, States.Events.StateEventType.Remove);
        }

        public Job GetJob()
        {
            if (Get().Job == 0)
                return null;

            return Job.MakeJob(_state, Get().Job);
        }

        public void UpdateJob()
        {
            var job = GetJob();
            if (job == null)
            {
                if (_orderManager.CurrentOrder != null)
                {
                    var recipe = _orderManager.CurrentOrder.Recipes.Where(x => x.IsOpen()).FirstOrDefault();
                    if (recipe != null)
                    {
                        job = SetJob(new RecipeJob(_state, recipe));
                    }
                    else
                    {
                        // all recipies is compited. do nothing
                    }
                }

            }
        }

        private Job SetJob(RecipeJob recipeJob)
        {
            var state = Get();
            if (state.Job != 0)
                throw new Exception("Job already setted");

            state.Job = recipeJob.Id;
            _state.Change(Id, state);
            return recipeJob;
        }

        public void FinishCurrentJob()
        {
            //if (MainHeroJob != null)
            //{
            //    MainHeroJob.Stop();
            //    MainHeroJob = null;
            //}
            //HandleOrder();
        }

        public struct GameState : IStateEntity
        {
            public uint Job { get; set; }
        }

        private void HandleUpdateConstruction(uint id, Construction.GameState state)
        {
            UpdateJob();
        }

        private void HandleUpdateOrder(uint id, CurrentOrder.GameState state)
        {
            UpdateJob();
        }
    }
}
