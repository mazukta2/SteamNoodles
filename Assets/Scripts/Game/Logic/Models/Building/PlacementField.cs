using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Units;
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
        private GameLevel _level;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition, Resources resources, GameLevel level)
        {
            ConstructionsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = definition ?? throw new ArgumentNullException(nameof(definition));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _level = level ?? throw new ArgumentNullException(nameof(level));
            

            Rect = new IntRect(-Size.X / 2, -Size.Y / 2, Size.X, Size.Y);
        }

        protected override void DisposeInner()
        {
            foreach (var item in _constructions)
                item.Dispose();
            _constructions.Clear();
        }


        public bool CanPlace(ConstructionCard card, IntPoint position, FieldRotation rotation)
        {
            return CanPlace(card.Definition, position, rotation);
        }

        public bool CanPlace(ConstructionDefinition definition, IntPoint position, FieldRotation rotation)
        {
            return definition
                .GetOccupiedSpace(position, rotation)
                .All(otherPosition => IsFreeCell(definition, otherPosition, rotation));
        }

        public Construction Build(ConstructionCard card, IntPoint position, FieldRotation rotation)
        {
            _resources.Points.Value += GetPoints(card.Definition, position, rotation);

            var construction = new Construction(this, card.Definition, position, rotation);
            _constructions.Add(construction);
            card.RemoveFromHand();

            _level.Turn();

            OnConstructionAdded(construction);
            return construction;
        }

        public bool IsFreeCell(ConstructionDefinition constructionDefinition, IntPoint position, FieldRotation rotation)
        {
            if (!Rect.IsInside(position))
                return false;

            if (Constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (constructionDefinition.Requirements.DownEdge)
            {
                var min = Rect.Y;
                var max = Rect.Y + constructionDefinition.GetRect(rotation).Height;
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

        public int GetPoints(ConstructionDefinition constructionDefinition, IntPoint position, FieldRotation rotation)
        {
            if (!CanPlace(constructionDefinition, position, rotation))
                return 0;

            var adjacentPoints = 0;
            foreach (var construction in GetAdjacentConstructions(constructionDefinition, position, rotation))
            {
                if (constructionDefinition.AdjacencyPoints.ContainsKey(construction.Definition))
                {
                    adjacentPoints += constructionDefinition.AdjacencyPoints[construction.Definition];
                }
            }

            return constructionDefinition.Points + adjacentPoints;
        }

        private IReadOnlyCollection<IntPoint> GetListOfAdjacentCells(ConstructionDefinition constructionDefinition, IntPoint position, FieldRotation rotation)
        {
            var list = new List<IntPoint>();

            foreach (var cell in constructionDefinition.GetOccupiedSpace(position, rotation))
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

        private IReadOnlyCollection<Construction> GetAdjacentConstructions(ConstructionDefinition constructionDefinition, IntPoint position, FieldRotation rotation)
        {
            var adjecentsCells = GetListOfAdjacentCells(constructionDefinition, position, rotation);
            var adjecentConstructions = new List<Construction>();
            foreach (var construction in _constructions)
            {
                foreach (var occupiedCell in construction.GetOccupiedScace())
                {
                    if (adjecentsCells.Any(x => x == occupiedCell))
                    {
                        if (!adjecentConstructions.Contains(construction))
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
