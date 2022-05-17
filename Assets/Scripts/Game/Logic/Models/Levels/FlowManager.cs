using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class FlowManager : Disposable
    {
        public event Action OnTurn = delegate { };
        public event Action<bool> OnWaveEnded = delegate { };

        private LevelDefinition _levelDefinition;
        private PlacementField _constructionsManager;
        private PlayerHand _hand;
        private Deck<ConstructionDefinition> _rewardDeck;

        public FlowManager(LevelDefinition levelDefinition, SessionRandom random, PlacementField constructionsManager, PlayerHand hand)
        {
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
            if (!CanNextWave())
                throw new Exception("Cant start next wave");

            while (_constructionsManager.Constructions.Count > 1)
            {
                _constructionsManager.Constructions.Last().Destroy();
            }

            GiveCards(PlayerHand.ConstructionSource.NewWave);

            OnWaveEnded(true);
        }

        public void FailWave()
        {
            if (!CanFailWave())
                throw new Exception("Cant fail wave");

            while (_constructionsManager.Constructions.Count > 1)
            {
                _constructionsManager.Constructions.Last().Destroy();
            }

            GiveCards(PlayerHand.ConstructionSource.NewWave);

            OnWaveEnded(false);
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
