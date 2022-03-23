using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Building
{
    public class PlacementField : Disposable
    {
        public event Action<Construction> OnConstructionAdded = delegate { };
    //    public event Action<Construction> OnConstructionRemoved = delegate { };

        public IReadOnlyCollection<Construction> Constructions => _constructions.AsReadOnly();

        private List<Construction> _constructions = new List<Construction>();

        public ConstructionsSettingsDefinition ConstructionsSettings { get; private set; }
        public IntPoint Size => _field.Size;
        public IntRect Rect { get; private set; }
        public IntPoint Offset { get; private set; }

        private PlacementFieldDefinition _field;
        private Resources _resources;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition, Resources resources)
        {
            ConstructionsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = definition ?? throw new ArgumentNullException(nameof(definition));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));

            Rect = new IntRect(-Size.X / 2, -Size.Y / 2, Size.X, Size.Y);
        }

        protected override void DisposeInner()
        {
            foreach (var item in _constructions)
                item.Dispose();
            _constructions.Clear();
        }


        public bool CanPlace(ConstructionCard card, IntPoint position)
        {
            return card.Definition
                .GetOccupiedSpace(position)
                .All(otherPosition => IsFreeCell(card.Definition, otherPosition));
        }

        public Construction Build(ConstructionCard card, IntPoint position)
        {
            _resources.Points += GetPoints(card.Definition, position);

            var construction = new Construction(this, card.Definition, position);
            _constructions.Add(construction);
            card.RemoveFromHand();

            OnConstructionAdded(construction);
            return construction;
        }

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

        private FloatPoint GetOffset()
        {
            var offset = FloatPoint.Zero;
            if (Size.X % 2 == 0)
                offset += new FloatPoint(0.5f, 0);
            if (Size.Y % 2 == 0)
                offset += new FloatPoint(0, 0.5f);

            return offset * ConstructionsSettings.CellSize;
        }

        public FloatPoint GetLocalPosition(IntPoint position)
        {
            return new FloatPoint(position.X * ConstructionsSettings.CellSize,
                   position.Y * ConstructionsSettings.CellSize) + GetOffset();
        }

        public int GetPoints(ConstructionDefinition constructionDefinition, IntPoint position)
        {
            var positions = constructionDefinition.GetOccupiedSpace(position);
            if (!positions.All(x => Rect.IsInside(position)))
                return 0;

            if (Constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return 0;


            var adjacentPoints = 0;
            foreach (var construction in GetAdjacentConstructions(constructionDefinition, position))
            {
                if (constructionDefinition.AdjacencyPoints.ContainsKey(construction.Definition))
                {
                    adjacentPoints += constructionDefinition.AdjacencyPoints[construction.Definition];
                }
            }

            return constructionDefinition.Points + adjacentPoints;
        }

        private IReadOnlyCollection<IntPoint> GetListOfAdjacentCells(ConstructionDefinition constructionDefinition, IntPoint position)
        {
            var list = new List<IntPoint>();

            foreach (var cell in constructionDefinition.GetOccupiedSpace(position))
            {
                AddCell(cell + new IntPoint(1, 0));
                AddCell(cell + new IntPoint(-1, 0));
                AddCell(cell + new IntPoint(0, 1));
                AddCell(cell + new IntPoint(0, -1));
            }

            return list.AsReadOnly();

            void AddCell(IntPoint point)
            {
                if (!Rect.IsInside(point))
                    return;

                if (list.Contains(point))
                    return;

                list.Add(point);
            }
        }

        private IReadOnlyCollection<Construction> GetAdjacentConstructions(ConstructionDefinition constructionDefinition, IntPoint position)
        {
            var adjecentsCells = GetListOfAdjacentCells(constructionDefinition, position);
            var adjecentConstructions = new List<Construction>();
            foreach (var construction in _constructions)
            {
                foreach (var occupiedCell in construction.GetOccupiedScace())
                {
                    if (adjecentsCells.Any(x => x == occupiedCell))
                    {
                        adjecentConstructions.Add(construction);
                    }
                }
            }
            return adjecentConstructions.AsReadOnly();
        }

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
        //public int GetConstructionsWithTag(ConstructionTag tag)
        //{
        //    return Constructions.Sum(x => x.GetTagsCount(tag));
        //}
    }
}
