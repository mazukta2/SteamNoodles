using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Functions.Constructions
{
    public static class ConstructionsFunctions
    {
        public static bool CanPlace(this IQuery<Construction> constructions, ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            return CanPlace(constructions, card.Scheme, position, rotation);
        }

        private static bool CanPlace(this IQuery<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            return scheme.Placement
                .GetOccupiedSpace(position, rotation)
                .All(otherPosition => constructions.IsAvailable(scheme, otherPosition, rotation));
        }
        
        public static IReadOnlyCollection<FieldPosition> GetUnoccupiedCells(this IQuery<Construction> constructions, Field field)
        {
            var list = new List<FieldPosition>();
            
            var constructionsList = constructions.Get();
            
            var boundaries = field.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(field, x, y);
                    
                    if (constructionsList.Any(otherBuilding => otherBuilding.GetOccupiedSpace()
                            .Any(pos => pos == fieldPosition)))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }
            
            return list.AsReadOnly();
        }
        
        public static bool IsAvailableToBuild(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var boundaries = position.Field.GetBoundaries();
            if (scheme.IsDownEdge())
            {
                var min = boundaries.Value.Y;
                var max = boundaries.Value.Y + scheme.Placement.GetHeight(rotation);
                return min <= position.Value.Y && position.Value.Y < max;
            }

            return true;
        }
        
        public static bool IsAvailable(this IQuery<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var freeCells = constructions.GetUnoccupiedCells(position.Field);
            var isAvailable = ConstructionsFunctions.IsAvailableToBuild(scheme, position, rotation);

            var isFreeCell = freeCells.Any(x => x.Value == position.Value);
            return (isFreeCell && isAvailable);
        }
        
        public static BuildingPoints GetPoints(this IQuery<Construction> constructions, 
            ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            if (!CanPlace(constructions, scheme, position, rotation))
                return new BuildingPoints(0);

            var adjacentPoints = BuildingPoints.Zero;
            foreach (var construction in GetAdjacentConstructions(constructions, scheme, position, rotation))
            {
                if (scheme.AdjacencyPoints.HasAdjacencyBonusWith(construction.Scheme))
                {
                    adjacentPoints += scheme.AdjacencyPoints.GetAdjacencyBonusWith(construction.Scheme);
                }
            }

            return scheme.Points + adjacentPoints;
        }

        private static IReadOnlyCollection<Construction> GetAdjacentConstructions(this IQuery<Construction> constructions, 
            ConstructionScheme scheme,
            FieldPosition position, FieldRotation rotation)
        {
            var adjacentCells = GetListOfAdjacentCells(scheme, position, rotation);
            var adjacentConstructions = new List<Construction>();
            foreach (var construction in constructions.Get())
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
        
        private static IReadOnlyCollection<FieldPosition> GetListOfAdjacentCells(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
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
                if (!position.IsInside())
                    return;

                if (list.Contains(point))
                    return;

                list.Add(point);
            }
        }
    }
}