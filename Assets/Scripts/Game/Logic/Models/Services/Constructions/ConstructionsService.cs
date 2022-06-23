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

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions
{
    public class ConstructionsService : Disposable, IService
    {
        private readonly IRepository<Construction> _constructions;
        private readonly Field _Field;

        public ConstructionsService(IRepository<Construction> constructions, Field Field)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _Field = Field ?? throw new ArgumentNullException(nameof(Field));
        }

        protected override void DisposeInner()
        {
        }
        
        public GameVector3 GetWorldPosition(Construction construction)
        {
            return _Field.GetWorldPosition(construction);
        }

        public BuildingPoints GetPoints(ConstructionCard card, CellPosition position, FieldRotation rotation)
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

        private IReadOnlyDictionary<Construction, BuildingPoints> GetAdjacencyPoints(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
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

        public IReadOnlyDictionary<Construction, BuildingPoints> GetAdjacencyPoints(ConstructionCard card, CellPosition position, FieldRotation rotation)
        {
            return GetAdjacencyPoints(card.Scheme, position, rotation);
        }

        public bool CanPlace(ConstructionCard card, CellPosition position, FieldRotation rotation)
        {
            return CanPlace(card.Scheme, position, rotation);
        }

        private bool CanPlace(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            return scheme.Placement
                .GetOccupiedSpace(position, rotation)
                .All(otherPosition => IsFreeCell(scheme, otherPosition, rotation));
        }

        public bool IsFreeCell(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            var boundaries = _Field.GetBoundaries();
            if (!boundaries.IsInside(position))
                return false;

            var constructions = _constructions.Get();
            if (constructions.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.IsDownEdge())
            {
                var min = boundaries.Value.Y;
                var max = boundaries.Value.Y + scheme.Placement.GetHeight(rotation);
                return min <= position.Value.Y && position.Value.Y < max;
            }

            return true;
        }

        public IReadOnlyCollection<CellPosition> GetAllOccupiedSpace()
        {
            var list = new List<CellPosition>();

            foreach (var construction in _constructions.Get())
                list.AddRange(construction.GetOccupiedScace());

            return list.AsReadOnly();
        }


        private IReadOnlyCollection<CellPosition> GetListOfAdjacentCells(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            var list = new List<CellPosition>();

            foreach (var cell in scheme.Placement.GetOccupiedSpace(position, rotation))
            {
                AddCell(cell + new CellPosition(1, 0));
                AddCell(cell + new CellPosition(-1, 0));
                AddCell(cell + new CellPosition(0, 1));
                AddCell(cell + new CellPosition(0, -1));
            }

            return list.AsReadOnly();

            void AddCell(CellPosition point)
            {
                if (!_Field.GetBoundaries().IsInside(point))
                    return;

                if (list.Contains(point))
                    return;

                list.Add(point);
            }
        }

        private IReadOnlyCollection<Construction> GetAdjacentConstructions(ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            var adjacentCells = GetListOfAdjacentCells(scheme, position, rotation);
            var adjacentConstructions = new List<Construction>();
            foreach (var construction in _constructions.Get())
            {
                foreach (var occupiedCell in construction.GetOccupiedScace())
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
