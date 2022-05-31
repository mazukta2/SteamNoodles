using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Flow
{
    public class StageFlowService : Disposable
    {
        //public event Action OnTurn = delegate { };
        //public event Action<bool> OnWaveEnded = delegate { };
        //public event Action OnDayFinished = delegate { };

        private readonly ConstructionsSettingsDefinition _constructionsDefinitions;
        private readonly LevelDefinition _levelDefinition;
        private readonly IGameRandom _random;
        private readonly IRepository<Construction> _constructions;
        private readonly HandService _hand;
        private readonly SchemesService _schemesService;
        private readonly SequenceManager _sequence = new SequenceManager();


        private Deck<ConstructionDefinition> _rewardDeck;
        private int _wave;

        public StageFlowService(ConstructionsSettingsDefinition constructionsDefinitions, LevelDefinition levelDefinition, IGameRandom random,
            IRepository<Construction> constructions, HandService hand, SchemesService schemesService)
        {
            _constructionsDefinitions = constructionsDefinitions ?? throw new ArgumentNullException(nameof(constructionsDefinitions));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _random = random;
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));
            _schemesService = schemesService;

            _rewardDeck = new Deck<ConstructionDefinition>(random);
            foreach (var item in levelDefinition.ConstructionsReward)
                _rewardDeck.Add(item.Key, item.Value);

            //Flow.OnTurn += TurnManager_OnTurn;
            //Flow.OnWaveEnded += TurnManager_OnWaveEnded;
            //Resources.Points.OnMaxTargetLevelUp += OnLevelUp;
        }

        protected override void DisposeInner()
        {
            //Resources.Points.OnMaxTargetLevelUp -= OnLevelUp;
            //Flow.OnTurn -= TurnManager_OnTurn;
            //Flow.OnWaveEnded -= TurnManager_OnWaveEnded;
            _sequence.Dispose();
        }

        private void OnLevelUp()
        {
            //Flow.GiveCards();
        }

        private void TurnManager_OnTurn()
        {
            //Queue.ServeCustomer();
        }

        private void TurnManager_OnWaveEnded(bool victory)
        {
            //if (victory)
            //    Queue.ServeAll();
            //else
            //{
            //    Queue.ClearQueue();
            //    Queue.FreeAll();
            //}
        }
        public void Turn()
        {
            //OnTurn();
        }

        public void WinWave()
        {
            if (_sequence.IsActive())
                return;

            if (!CanNextWave())
                throw new Exception("Cant start next wave");

            _wave++;
            if (_levelDefinition.Waves <= _wave)
            {
                //OnDayFinished();
                return;
            }

            var constructions = _constructions.Get().ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {

                _sequence.Add(new DestroyConstructionStep(_constructions, constructions[i], IGameTime.Default, _constructionsDefinitions.ConstructionDestroyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                GiveCards();
                //OnWaveEnded(true);
            }
        }

        public void Start()
        {
            foreach (var item in _levelDefinition.StartingHand)
            {
                _hand.Add(_schemesService.Find(item));
            }
        }

        public void FailWave()
        {
            if (_sequence.IsActive())
                return;

            if (!CanFailWave())
                throw new Exception("Cant fail wave");

            _wave++;
            if (_levelDefinition.Waves <= _wave)
            {
                //OnDayFinished();
                return;
            }

            var constructions = _constructions.Get().ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {
                _sequence.Add(new DestroyConstructionStep(_constructions, constructions[i], IGameTime.Default, _constructionsDefinitions.ConstructionDestroyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                GiveCards();
                //OnWaveEnded(false);
            }
        }

        public void GiveCards()
        {
            if (_rewardDeck.IsEmpty())
                return;

            for (int i = 0; i < 3; i++)
            {
                var constrcution = _schemesService.TakeRandom(_random);
                _hand.Add(constrcution);
            }
        }

        public bool CanProcessNextWave()
        {
            if (_constructions.Get().Count < 1)
                return false;

            return true;
        }

        public bool CanNextWave()
        {
            if (!CanProcessNextWave())
                return false;

            if (_constructions.Get().Count < _levelDefinition.ConstructionsForNextWave)
                return false;

            return true;
        }

        public bool CanFailWave()
        {
            if (CanNextWave())
                return false;

            if (_constructions.Get().Count < 1)
                return false;

            if (_hand.GetCards().Count > 0)
                return false;

            return true;
        }

        public float GetWaveProgress()
        {
            return Math.Min(1, _constructions.Get().Count / (float)_levelDefinition.ConstructionsForNextWave);
        }

    }
}
