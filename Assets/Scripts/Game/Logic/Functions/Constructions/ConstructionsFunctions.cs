using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Functions.Constructions
{
    public static class ConstructionsFunctions
    {

        public static GroupOfPositions GetAvailableToBuildCells(this IEntityList<Construction> constructions,
            Field field, ConstructionScheme scheme, FieldRotation rotation)
        {
            var list = new HashSet<FieldPosition>();
            var occupiedSpace = constructions.Get()
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace());
            
            var boundaries = field.GetBoundaries();
            var minY = boundaries.Value.Y;
            var maxY = boundaries.Value.Y + boundaries.Value.Height;
            
            if (scheme.IsDownEdge())
                maxY = boundaries.Value.Y + scheme.Placement.GetHeight(rotation);

            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(field, x, y);
                    
                    if (!(minY <= y && y < maxY))
                        continue;

                    if (occupiedSpace.Contains(fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }

            return new GroupOfPositions(list);
        }


        public static bool CanPlace(this IEntityList<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var available = GetAvailableToBuildCells(constructions, position.Field, scheme, rotation);
            var occupiedCells = scheme.Placement.GetOccupiedSpace(position, rotation);
            return occupiedCells.All(occupiedPosition => available.Cells.Contains(occupiedPosition));
        }
        
        public static GroupOfPositions GetUnoccupiedCells(this IEntityList<Construction> constructions, Field field)
        {
            var list = new List<FieldPosition>();
            
            var constructionsList = constructions.Get();
            var occupiedSpace = constructionsList
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace());
            
            var boundaries = field.GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(field, x, y);
                    if (occupiedSpace.Any(pos => pos == fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }
            
            return new GroupOfPositions(list);
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

        public static BuildingPoints GetPoints(this IEntityList<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            if (!constructions.CanPlace(scheme, position, rotation))
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

        private static IReadOnlyCollection<Construction> GetAdjacentConstructions(this IEntityList<Construction> constructions, 
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