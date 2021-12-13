using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Rewards;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Rewards
{
    public class RewardCalculator : Disposable
    {
        private GameLevel _level;
        private SessionRandom _random;

        public RewardCalculator(GameLevel level, SessionRandom random)
        {
            _level = level ?? throw new ArgumentNullException(nameof(level));
            _random = random ?? throw new ArgumentNullException(nameof(random));
        }

        protected override void DisposeInner()
        {
        }


        public void Give(IReward reward)
        {
            GiveCardsToHand(reward);
        }

        private void GiveCardsToHand(IReward reward)
        {
            var count = _random.GetRandom(reward.MinToHand, reward.MaxToHand);
            var deck = new Deck<IConstructionSettings>(_random);
            if (reward.ToHand.Count == 0)
                return;

            foreach (var item in reward.ToHand)
                deck.Add(item.Key, item.Value);

            var result = new List<IConstructionSettings>();
            for (int i = 0; i < count; i++)
                result.Add(deck.Take());

            foreach (var item in result)
                _level.Hand.Add(item);
        }
    }
}
