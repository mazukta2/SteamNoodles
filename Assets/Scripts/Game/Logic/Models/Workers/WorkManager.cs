using Game.Assets.Scripts.Game.Logic.Models.Orders;
using Game.Assets.Scripts.Game.Logic.States;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Workers
{
    public class WorkManager
    {
        private State _state;
        private OrderManager _orderManager;
        private uint _id;

        private GameState Get() => _state.Get<GameState>(_id);

        public WorkManager(State state, OrderManager orderManager)
        {
            _state = state;
            _orderManager = orderManager;
            (_id, _) =_state.Add(new GameState());
            var mainWorker = new Worker(state, orderManager);
            SetMainWorker(mainWorker);
        }

        public Worker GetMainWorker()
        {
            return new Worker(_state, Get().MainWorker, _orderManager);
        }

        public void SetMainWorker(Worker worker)
        {
            var st = Get();
            st.MainWorker = worker.Id;
            _state.Change(_id, st);
        }

        public struct GameState : IStateEntity
        {
            public uint MainWorker { get; set; }
        }
    }
}
