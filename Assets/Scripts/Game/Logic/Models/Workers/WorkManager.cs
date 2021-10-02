using Game.Assets.Scripts.Game.Logic.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class WorkManager
    {
        private GameState _state;

        public WorkManager(OrderManager orderManager, Placement placement, GameTime gameTime)
        {
            _state = new GameState();
            _state.Orders = orderManager;
            _state.Placements = placement;
            _state.GameTime = gameTime;
            MainWorker = new Worker(orderManager, placement, gameTime);
        }

        public WorkManager(GameState state)
        {
            _state = state;
            //var mainWorker = new Worker(state, _orderManager, _placement, _time);
            //MainWorker = mainWorker;
        }

        public Worker MainWorker { get => _state.MainWorker; set => _state.MainWorker = value; }

        public class GameState : IStateEntity
        {
            public Worker MainWorker { get; set; }
            public OrderManager Orders { get; set; }
            public Placement Placements { get; set; }
            public GameTime GameTime { get; set; }
        }
    }
}
