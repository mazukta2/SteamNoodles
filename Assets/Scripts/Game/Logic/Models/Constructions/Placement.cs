using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Rewards;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions;
using Game.Assets.Scripts.Game.Logic.Settings.Constructions.Features;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Buildings
{
    public class Placement : Disposable
    {
        public readonly float CellSize = 0.25f;

        public event Action<Construction> OnConstructionAdded = delegate { };
        public event Action<Construction> OnConstructionRemoved = delegate { };

        public Point Size => _prototype.Size;
        public Rect Rect => Size.AsCenteredRect();
        public FloatRect RealRect => Rect * CellSize;
        public IReadOnlyCollection<Construction> Constructions => _constructions.AsReadOnly();

        private IPlacementSettings _prototype { get; set; }
        private PlayerHand _hand { get; set; }

        private RewardCalculator _rewardCalculator;
        private List<Construction> _constructions = new List<Construction>();

        public Placement(IPlacementSettings placement, PlayerHand hand, RewardCalculator rewardCalculator)
        {
            _prototype = placement;
            _hand = hand;
            _rewardCalculator = rewardCalculator;
        }

        protected override void DisposeInner()
        {
            foreach (var construction in _constructions)
                construction.Dispose();
            _constructions = null;
        }

        public Construction Build(ConstructionCard card, Point position)
        {
            var construction = new Construction(card.Settings, position);
            _constructions.Add(construction);
            if (_hand.Contain(card))
                _hand.Remove(card);

            foreach (var item in construction.GetFeatures().OfType<IGiveRewardOnBuildConstructionFeatureSettings>())
                _rewardCalculator.Give(item.Reward);

            OnConstructionAdded(construction);
            return construction;
        }

        public bool Contain(Construction key)
        {
            return Constructions.Any(x => x == key);
        }

        public IReadOnlyCollection<Construction> GetConstructionsWithFeature<T>() where T : IConstructionFeatureSettings
        {
            return Constructions.Where(x => x.GetFeatures().OfType<T>().Any()).AsReadOnly();
        }

        public bool HasConstructionsWithFeature<T>() where T : IConstructionFeatureSettings
        {
            return GetConstructionsWithFeature<T>().Any();
        }

        public bool CanPlace(ConstructionCard scheme, Point position)
        {
            return scheme
                .GetOccupiedSpace(position)
                .All(otherPosition => IsFreeCell(scheme, otherPosition));
        }

        public bool IsFreeCell(ConstructionCard scheme, Point position)
        {
            if (!Rect.IsInside(position))
                return false;

            if (Constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.Requirements.DownEdge)
            {
                var min = Rect.Y;
                var max = Rect.Y + scheme.CellSize.Y;
                return min <= position.Y && position.Y < max;
            }

            return true;
        }

        public int GetConstructionsWithTag(ConstructionTag tag)
        {
            return Constructions.Sum(x => x.GetTagsCount(tag));
        }

    }
}
