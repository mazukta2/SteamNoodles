using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Animations;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class FlowManager : Disposable
    {
        public event Action OnTurn = delegate { };
        public event Action<bool> OnWaveEnded = delegate { };
        public event Action OnDayFinished = delegate { };

        private readonly ConstructionsSettingsDefinition _constructionsDefinitions;
        private MainLevelVariation _levelDefinition;
        private PlacementField _constructionsManager;
        private PlayerHand _hand;
        private Deck<ConstructionDefinition> _rewardDeck;
        private int _wave;
        private SequenceManager _sequence = new SequenceManager();

        public FlowManager(ConstructionsSettingsDefinition constructions, MainLevelVariation levelDefinition, IGameRandom random, PlacementField constructionsManager, PlayerHand hand)
        {
            _constructionsDefinitions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _levelDefinition = levelDefinition ?? throw new ArgumentNullException(nameof(levelDefinition));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _hand = hand ?? throw new ArgumentNullException(nameof(hand));
            _constructionsManager.OnConstructionBuilded += Placement_OnConstructionBuilded;

            _rewardDeck = new Deck<ConstructionDefinition>(random);
            foreach (var item in levelDefinition.ConstructionsReward)
                _rewardDeck.Add(item.Key, item.Value);
        }

        protected override void DisposeInner()
        {
            _sequence.Dispose();
            _constructionsManager.OnConstructionBuilded -= Placement_OnConstructionBuilded;
        }

        private void Placement_OnConstructionBuilded(Constructions.Construction obj)
        {
            Turn();
        }

        public void Turn()
        {
            OnTurn();
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
                OnDayFinished();
                return;
            }

            var constructions = _constructionsManager.Constructions.ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {
                _sequence.Add(new DestroyConstructionStep(constructions[i], IGameTime.Default, _constructionsDefinitions.ConstructionDestroyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                GiveCards(PlayerHand.ConstructionSource.NewWave);

                OnWaveEnded(true);
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
                OnDayFinished();
                return;
            }

            var constructions = _constructionsManager.Constructions.ToArray();
            for (int i = constructions.Length - 1; i >= 1; i--)
            {
                _sequence.Add(new DestroyConstructionStep(constructions[i], IGameTime.Default, _constructionsDefinitions.ConstructionDestroyTime));
            }

            _sequence.Add(new ActionStep(EndWave));
            _sequence.ProcessSteps();

            void EndWave()
            {
                GiveCards(PlayerHand.ConstructionSource.NewWave);

                OnWaveEnded(false);
            }
        }

        public void GiveCards(PlayerHand.ConstructionSource source)
        {
            if (_rewardDeck.IsEmpty())
                return;

            for (int i = 0; i < 3; i++)
            {
                var constrcution = _rewardDeck.Take();
                _hand.Add(constrcution, source);
            }
        }

        public bool CanProcessNextWave()
        {
            if (_constructionsManager.Constructions.Count < 1)
                return false;

            return true;
        }

        public bool CanNextWave()
        {
            if (!CanProcessNextWave())
                return false;

            if (_constructionsManager.Constructions.Count < _levelDefinition.ConstructionsForNextWave)
                return false;

            return true;
        }

        public bool CanFailWave()
        {
            if (CanNextWave())
                return false;

            if (_constructionsManager.Constructions.Count < 1)
                return false;

            if (_hand.Cards.Count > 0)
                return false;

            return true;
        }

        public float GetWaveProgress()
        {
            return Math.Min(1, _constructionsManager.Constructions.Count / (float)_levelDefinition.ConstructionsForNextWave);
        }

    }
}
