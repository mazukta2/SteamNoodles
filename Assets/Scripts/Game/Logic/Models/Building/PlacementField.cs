using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Building
{
    public class PlacementField : Disposable
    {
    //    public event Action<Construction> OnConstructionAdded = delegate { };
    //    public event Action<Construction> OnConstructionRemoved = delegate { };

    //    public Point Size => _prototype.Size;
    //    public Rect Rect => Size.AsCenteredRect();
    //    public FloatRect RealRect => Rect * CellSize;
        public IReadOnlyCollection<Construction> Constructions => _constructions.AsReadOnly();

    //    private IPlacementSettings _prototype { get; set; }
    //    private PlayerHand _hand { get; set; }

    //    private RewardCalculator _rewardCalculator;
        private List<Construction> _constructions = new List<Construction>();

        public ConstructionsSettingsDefinition ConstructionsSettings { get; private set; }
        public IntPoint Size => _field.Size;
        public IntRect Rect { get; private set; }
        private PlacementFieldDefinition _field;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition)
        {
            ConstructionsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = definition ?? throw new ArgumentNullException(nameof(definition));

            Rect = new IntRect(-Size.X / 2, -Size.Y / 2, Size.X, Size.Y);
        }

        protected override void DisposeInner()
        {
        }

        //public Construction Build(ConstructionCard card, Point position)
        //{
        //    var construction = new Construction(card.Settings, position);
        //    _constructions.Add(construction);
        //    if (_hand.Contain(card))
        //        _hand.Remove(card);

        //    foreach (var item in construction.GetFeatures().OfType<IGiveRewardOnBuildConstructionFeatureSettings>())
        //        _rewardCalculator.Give(item.Reward);

        //    OnConstructionAdded(construction);
        //    return construction;
        //}

        //public bool Contain(Construction key)
        //{
        //    return Constructions.Any(x => x == key);
        //}

        //public IReadOnlyCollection<Construction> GetConstructionsWithFeature<T>() where T : IConstructionFeatureSettings
        //{
        //    return Constructions.Where(x => x.GetFeatures().OfType<T>().Any()).AsReadOnly();
        //}

        //public bool HasConstructionsWithFeature<T>() where T : IConstructionFeatureSettings
        //{
        //    return GetConstructionsWithFeature<T>().Any();
        //}

        //public bool CanPlace(ConstructionCard scheme, Point position)
        //{
        //    return scheme
        //        .GetOccupiedSpace(position)
        //        .All(otherPosition => IsFreeCell(scheme, otherPosition));
        //}

        public bool IsFreeCell(ConstructionDefinition constructionDefinition, IntPoint position)
        {
            if (!Rect.IsInside(position))
                return false;

            if (Constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (constructionDefinition.Requirements.DownEdge)
            {
                var min = Rect.Y;
                var max = Rect.Y + constructionDefinition.Size.Y;
                return min <= position.Y && position.Y < max;
            }

            return true;
        }

        //public int GetConstructionsWithTag(ConstructionTag tag)
        //{
        //    return Constructions.Sum(x => x.GetTagsCount(tag));
        //}
    }
}
