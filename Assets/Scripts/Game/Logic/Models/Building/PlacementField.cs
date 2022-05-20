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
        public event Action<Construction> OnConstructionRemoved = delegate { };
        public event Action<Construction> OnConstructionBuilded = delegate { };
        public event Action<Construction, int> OnPointChangedDuringConstruction = delegate { };

        public IReadOnlyCollection<Construction> Constructions => _constructions.AsReadOnly();

        private List<Construction> _constructions = new List<Construction>();

        public ConstructionsSettingsDefinition ConstructionsSettings { get; private set; }
        public IntPoint Size => _field.Size;
        public IntRect Rect { get; private set; }

        private PlacementFieldDefinition _field;
        private Resources _resources;

        public PlacementField(ConstructionsSettingsDefinition settings, PlacementFieldDefinition definition, Resources resources)
        {
            ConstructionsSettings = settings ?? throw new ArgumentNullException(nameof(settings));
            _field = definition ?? throw new ArgumentNullException(nameof(definition));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));

            if (Size.X % 2 == 0 || Size.Y % 2 == 0)
                throw new Exception("Even sized fields are not supported. Sorry :(");

            Rect = new IntRect(-Size.X / 2, -Size.Y / 2, Size.X, Size.Y);
        }

        protected override void DisposeInner()
        {
            foreach (var item in _constructions.ToList())
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
            var points = GetPoints(card.Definition, position, rotation);

            var construction = new Construction(ConstructionsSettings, card.Definition, position, rotation);
            construction.OnDispose += Construction_OnDispose;
            _constructions.Add(construction);
            card.Remove(1);
            OnConstructionAdded(construction);

            _resources.Points.Value += points;

            OnConstructionBuilded(construction);
            OnPointChangedDuringConstruction(construction, points);

            return construction;

            void Construction_OnDispose()
            {
                construction.OnDispose -= Construction_OnDispose;
                _constructions.Remove(construction);
                OnConstructionRemoved(construction);
            }
        }

        public IReadOnlyCollection<IntPoint> GetAllOccupiedSpace()
        {
            var list = new List<IntPoint>();

            foreach (var construction in Constructions)
                list.AddRange(construction.GetOccupiedScace());

            return list.AsReadOnly();
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

        public IReadOnlyDictionary<Construction, int> GetAdjacencyPoints(ConstructionDefinition constructionDefinition, IntPoint position, FieldRotation rotation)
        {
            var result = new Dictionary<Construction, int>();
            if (!CanPlace(constructionDefinition, position, rotation))
                return result;

            foreach (var construction in GetAdjacentConstructions(constructionDefinition, position, rotation))
            {
                if (constructionDefinition.AdjacencyPoints.ContainsKey(construction.Definition))
                {
                    result.Add(construction, constructionDefinition.AdjacencyPoints[construction.Definition]);
                }
            }

            return result.AsReadOnly();
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
    }
}
