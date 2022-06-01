using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Customers.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow.Animations;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
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
        private readonly StageLevel _level;
        private readonly HandService _hand;
        private readonly BuildingPointsService _points;
        private readonly int _giveCardsAmount;
        private readonly SchemesService _schemesService;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _wave;

        public StageFlowService(StageLevel level, HandService handService, SchemesService schemesService,
            BuildingPointsService buildingPointsService, int giveCardsAmount = 3)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _hand = handService ?? throw new ArgumentNullException(nameof(handService));
            _points = buildingPointsService ?? throw new ArgumentNullException(nameof(buildingPointsService));
            _giveCardsAmount = giveCardsAmount;
            _schemesService = schemesService ?? throw new ArgumentNullException(nameof(schemesService));
            //_rewardDeck = new DeckService<ConstructionDefinition>(random);
            //foreach (var item in levelDefinition.ConstructionsReward)
            //    _rewardDeck.Add(item.Key, item.Value);

            //Flow.OnTurn += TurnManager_OnTurn;
            //Flow.OnWaveEnded += TurnManager_OnWaveEnded;
            //Resources.Points.OnMaxTargetLevelUp += OnLevelUp;

            _points.OnMaxTargetLevelUp += HandleOnLevelUp;
        }

        //private StageFlowService(ConstructionsSettingsDefinition constructionsDefinitions, LevelDefinition levelDefinition, IGameRandom random,
        //    IRepository<Construction> constructions, HandService hand, SchemesService schemesService)
        //{
        //    _constructionsDefinitions = constructionsDefinitions ?? throw new ArgumentNullException(nameof(constructionsDefinitions));
        //    _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
        //    _random = random;
        //    _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
        //    _hand = hand ?? throw new ArgumentNullException(nameof(hand));
        //    _schemesService = schemesService;

        //    _rewardDeck = new DeckService<ConstructionDefinition>(random);
        //    foreach (var item in levelDefinition.ConstructionsReward)
        //        _rewardDeck.Add(item.Key, item.Value);

        //    //Flow.OnTurn += TurnManager_OnTurn;
        //    //Flow.OnWaveEnded += TurnManager_OnWaveEnded;
        //    //Resources.Points.OnMaxTargetLevelUp += OnLevelUp;
        //}

        protected override void DisposeInner()
        {
            _points.OnMaxTargetLevelUp -= HandleOnLevelUp;

            //Resources.Points.OnMaxTargetLevelUp -= OnLevelUp;
            //Flow.OnTurn -= TurnManager_OnTurn;
            //Flow.OnWaveEnded -= TurnManager_OnWaveEnded;
            _sequence.Dispose();
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

        private void HandleOnLevelUp()
        {
            GiveCards();
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
            foreach (var scheme in _level.StartingSchemes)
                _hand.Add(scheme);
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

        private void GiveCards()
        {
            for (int i = 0; i < _giveCardsAmount; i++)
            {
                var constrcution = _schemesService.TakeRandom();
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
