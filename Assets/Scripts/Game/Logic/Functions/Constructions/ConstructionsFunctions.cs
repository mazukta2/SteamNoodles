using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Functions.Constructions
{
    public static class ConstructionsFunctions
    {
        public static bool CanPlace(this IRepository<Construction> constructions, ConstructionCard card, FieldPosition position, FieldRotation rotation)
        {
            return CanPlace(constructions, card.Scheme, position, rotation);
        }

        private static bool CanPlace(this IRepository<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            return scheme.Placement
                .GetOccupiedSpace(position, rotation)
                .All(otherPosition => constructions.IsAvailable(scheme, otherPosition, rotation));
        }
        
        public static IReadOnlyCollection<FieldPosition> GetUnoccupiedCells(this IRepository<Construction> constructions, Field field)
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
        
        public static bool IsAvailable(this IRepository<Construction> constructions, ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var freeCells = constructions.GetUnoccupiedCells(position.Field);
            var isAvailable = ConstructionsFunctions.IsAvailableToBuild(scheme, position, rotation);

            var isFreeCell = freeCells.Any(x => x.Value == position.Value);
            return (isFreeCell && isAvailable);
        }
    }
}