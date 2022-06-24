using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class ConstructionsService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly Field _field;

        public ConstructionsService(IRepository<Construction> constructions, Field field)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }

        protected override void DisposeInner()
        {
        }
        
        public BuildingPoints GetPoints(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            var scheme = card.Scheme;
            if (!CanPlace(scheme, position, rotation))
                return new BuildingPoints(0);

            var adjacentPoints = BuildingPoints.Zero;
            foreach (var construction in GetAdjacentConstructions(scheme, position, rotation))
            {
                if (scheme.AdjacencyPoints.HasAdjacencyBonusWith(construction.Scheme))
                {
                    adjacentPoints += scheme.AdjacencyPoints.GetAdjacencyBonusWith(construction.Scheme);
                }
            }

            return scheme.Points + adjacentPoints;
        }

        private IReadOnlyDictionary<Construction, BuildingPoints> GetAdjacencyPoints(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var result = new Dictionary<Construction, BuildingPoints>();
            if (!CanPlace(scheme, position, rotation))
                return result;

            foreach (var construction in GetAdjacentConstructions(scheme, position, rotation))
            {
                if (scheme.AdjacencyPoints.HasAdjacencyBonusWith(construction.Scheme))
                {
                    result.Add(construction, scheme.AdjacencyPoints.GetAdjacencyBonusWith(construction.Scheme));
                }
            }

            return result.AsReadOnly();
        }

        public IReadOnlyDictionary<Construction, BuildingPoints> GetAdjacencyPoints(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            return GetAdjacencyPoints(card.Scheme, position, rotation);
        }

        public bool CanPlace(ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            return CanPlace(card.Scheme, position, rotation);
        }

        private bool CanPlace(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            return scheme.Placement
                .GetOccupiedSpace(position, rotation)
                .All(otherPosition => _constructions.IsAvailable(scheme, otherPosition, rotation));
        }

        private IReadOnlyCollection<FieldPosition> GetListOfAdjacentCells(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var list = new List<FieldPosition>();

            foreach (var cell in scheme.Placement.GetOccupiedSpace(position, rotation))
            {
                AddCell(cell + new CellPosition(1, 0));
                AddCell(cell + new CellPosition(-1, 0));
                AddCell(cell + new CellPosition(0, 1));
                AddCell(cell + new CellPosition(0, -1));
            }

            return list.AsReadOnly();

            void AddCell(FieldPosition point)
            {
                if (!_field.GetBoundaries().IsInside(point))
                    return;

                if (list.Contains(point))
                    return;

                list.Add(point);
            }
        }

        private IReadOnlyCollection<Construction> GetAdjacentConstructions(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var adjacentCells = GetListOfAdjacentCells(scheme, position, rotation);
            var adjacentConstructions = new List<Construction>();
            foreach (var construction in _constructions.Get())
            {
                foreach (var occupiedCell in construction.GetOccupiedSpace())
                {
                    if (adjacentCells.Any(x => x == occupiedCell))
                    {
                        if (!adjacentConstructions.Contains(construction))
                            adjacentConstructions.Add(construction);
                    }
                }
            }
            return adjacentConstructions.AsReadOnly();
        }
    }
}
