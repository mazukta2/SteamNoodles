using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Flow
{
    public class StageWaveService : Disposable
    {
        public event Action OnDayFinished = delegate { };

        private readonly IRepository<Construction> _constructions;
        private readonly HandService _hand;
        private readonly RewardsService _rewardsService;
        private readonly IGameTime _time;
        private readonly int _totalWaves;
        private readonly int _constructionsToEndWave;
        private readonly float _destoyTime;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _wave;

        public StageWaveService(IRepository<Construction> constructions, 
            HandService handService, 
            RewardsService rewardsService,
            IGameTime time,
            int waves = 3,
            int constructionsToEndWave = 6,
            float destoyTime = 0)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _hand = handService ?? throw new ArgumentNullException(nameof(handService));
            _rewardsService = rewardsService ?? throw new ArgumentNullException(nameof(rewardsService));
            _time = time;
            _totalWaves = waves;
            _constructionsToEndWave = constructionsToEndWave;
            _destoyTime = destoyTime;
        }

        protected override void DisposeInner()
        {
            _sequence.Dispose();
        }

        //private void TurnManager_OnWaveEnded(bool victory)
        //{
        //    //if (victory)
        //    //    Queue.ServeAll();
        //    //else
        //    //{
        //    //    Queue.ClearQueue();
        //    //    Queue.FreeAll();
        //    //}
        //}

        public void WinWave()
        {
            if (_sequence.IsActive())
                return;

            if (!CanWinWave())
                throw new Exception("Cant start next wave");

            _wave++;
            if (_totalWaves <= _wave)
            {
                OnDayFinished();
                return;
            }

            var constructions = _constructions.Get().ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {
                _sequence.Add(new DestroyConstructionStep(_constructions, constructions[i], _time, _destoyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                _rewardsService.GiveCards();
                //OnWaveEnded(true);
            }
        }

        public void FailWave()
        {
            if (_sequence.IsActive())
                return;

            if (!CanFailWave())
                throw new Exception("Cant fail wave");

            _wave++;
            if (_totalWaves <= _wave)
            {
                OnDayFinished();
                return;
            }

            var constructions = _constructions.Get().ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {
                _sequence.Add(new DestroyConstructionStep(_constructions, constructions[i], _time, _destoyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                _rewardsService.GiveCards();
                //OnWaveEnded(false);
            }
        }

        public bool IsActive()
        {
            if (_constructions.Get().Count < 1)
                return false;

            return true;
        }


        public bool CanWinWave()
        {
            if (_constructions.Get().Count < 1)
                return false;

            if (_constructions.Get().Count < _constructionsToEndWave)
                return false;

            return true;
        }

        public bool CanFailWave()
        {
            if (CanWinWave())
                return false;

            if (_constructions.Get().Count < 1)
                return false;

            if (_hand.GetCards().Count > 0)
                return false;

            return true;
        }

        public float GetWaveProgress()
        {
            return Math.Min(1, _constructions.Get().Count / (float)_constructionsToEndWave);
        }

    }
}
