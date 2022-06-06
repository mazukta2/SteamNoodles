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
    public class StageWaveService : Disposable
    {
        //public event Action OnTurn = delegate { };
        //public event Action<bool> OnWaveEnded = delegate { };
        //public event Action OnDayFinished = delegate { };

        private readonly ConstructionsSettingsDefinition _constructionsDefinitions;
        private readonly LevelDefinition _levelDefinition;
        private readonly IGameRandom _random;
        private readonly IRepository<Construction> _constructions;
        private readonly StageLevel _level;
        private readonly BuildingService _buildingService;
        private readonly HandService _hand;
        private readonly BuildingPointsService _points;
        private readonly int _giveCardsAmount;
        private readonly SchemesService _schemesService;
        private readonly SequenceManager _sequence = new SequenceManager();
        private int _wave;

        public StageWaveService(StageLevel level, 
            BuildingService buildingService,
            HandService handService, 
            SchemesService schemesService,
            BuildingPointsService buildingPointsService, int giveCardsAmount = 3)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _buildingService = buildingService ?? throw new ArgumentNullException(nameof(buildingService));
            _hand = handService ?? throw new ArgumentNullException(nameof(handService));
            _points = buildingPointsService ?? throw new ArgumentNullException(nameof(buildingPointsService));
            _giveCardsAmount = giveCardsAmount;
            _schemesService = schemesService ?? throw new ArgumentNullException(nameof(schemesService));

            _points.OnMaxTargetLevelUp += HandleOnLevelUp;
            _buildingService.OnBuild += HandleOnBuild;
        }

        protected override void DisposeInner()
        {
            _points.OnMaxTargetLevelUp -= HandleOnLevelUp;
            _buildingService.OnBuild -= HandleOnBuild;
            _sequence.Dispose();
        }

        private void Turn()
        {
            //var construction = _constructions.Get().First();
            //var queueStartingPosition = _fieldPositionService.GetWorldPosition(construction).X;
            //return new GameVector3(queueStartingPosition, 0, _levelDefinition.QueuePosition.Z);
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

        private void HandleOnLevelUp()
        {
            GiveCards();
        }

        private void HandleOnBuild(Construction obj)
        {
            Turn();
        }

    }
}
