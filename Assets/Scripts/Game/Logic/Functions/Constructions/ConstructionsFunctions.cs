using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Functions.Constructions
{
    public static class ConstructionsFunctions
    {
        
        public static IReadOnlyCollection<CellPosition> GetAllOccupiedSpace(this IRepository<Construction> constructions)
        {
            var list = new List<CellPosition>();

            foreach (var construction in constructions.Get())
                list.AddRange(construction.GetOccupiedScace());

            return list.AsReadOnly();
        }
        
        public static bool IsFreeCell(this IRepository<Construction> constructions, 
            Field field, ConstructionScheme scheme, CellPosition position, FieldRotation rotation)
        {
            var boundaries = field.GetBoundaries();
            if (!boundaries.IsInside(position))
                return false;

            var constructionsList = constructions.Get();
            if (constructionsList.Any(otherBuilding => otherBuilding.GetOccupiedScace().Any(pos => pos == position)))
                return false;

            if (scheme.IsDownEdge())
            {
                var min = boundaries.Value.Y;
                var max = boundaries.Value.Y + scheme.Placement.GetHeight(rotation);
                return min <= position.Value.Y && position.Value.Y < max;
            }

            return true;
        }
    }
}