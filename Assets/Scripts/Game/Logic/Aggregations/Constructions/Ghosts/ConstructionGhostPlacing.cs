using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts
{
    public class ConstructionGhostPlacing : Disposable, IAggregation
    {
        public Uid Id => _constructionGhost.Id;
        
        private readonly IDatabase<Construction> _constructions;
        private readonly Field _field;
        private readonly ConstructionGhost _constructionGhost;

        public ConstructionGhostPlacing(ConstructionGhost ghost, IDatabase<Construction> constructions, Field field)
        {
            _constructions = constructions;
            _field = field;
            _constructionGhost = ghost;
        }
        
        public Construction Build()
        {
            if (!CanBuild())
                throw new Exception("Can't build construction here");

            var construction = new Construction(_constructionGhost.Card.Scheme, 
                _constructionGhost.Position, 
                _constructionGhost.Rotation);
            var points = GetPoints();

            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points, _constructionGhost.Card));

            return construction;
        }
        
        public void TryBuild()
        {
            if (CanBuild())
                Build();
        }

        public bool CanBuild()
        {
            var occupiedCells = _constructionGhost.Card.Scheme.Placement
                .GetOccupiedSpace(_constructionGhost.Position, _constructionGhost.Rotation);
            return occupiedCells.All(occupiedPosition => GetAvailableToBuildCells().Cells.Contains(occupiedPosition));
        }

        public FieldRotation GetRotation()
        {
            return _constructionGhost.Rotation;
        }

        public BuildingPoints GetPoints()
        {
            if (!CanBuild())
                return new BuildingPoints(0);

            var adjacentPoints = BuildingPoints.Zero;
            foreach (var construction in GetAdjacentConstructions(_constructionGhost.Card.Scheme, 
                         _constructionGhost.Position, _constructionGhost.Rotation))
            {
                if (_constructionGhost.Card.Scheme.AdjacencyPoints.HasAdjacencyBonusWith(construction.Scheme))
                {
                    adjacentPoints += _constructionGhost.Card.Scheme.AdjacencyPoints.GetAdjacencyBonusWith(construction.Scheme);
                }
            }

            return _constructionGhost.Card.Scheme.Points + adjacentPoints;
        }

        public void SetPosition(GameVector3 pointerPosition)
        {
            var size = _constructionGhost.Card.Scheme.Placement.GetRect(_constructionGhost.Rotation);
            var fieldPosition = _field.GetFieldPosition(pointerPosition, size);
            _constructionGhost.SetPosition(fieldPosition, pointerPosition);
        }

        public void SetRotation(FieldRotation rotation)
        {
            _constructionGhost.SetRotation(rotation);
        }

        private IReadOnlyCollection<Construction> GetAdjacentConstructions( 
            ConstructionScheme scheme,
            FieldPosition position, FieldRotation rotation)
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
        
        private GroupOfPositions GetAvailableToBuildCells()
        {
            var list = new HashSet<FieldPosition>();
            var occupiedSpace = _constructions.Get()
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace()).AsReadOnly();
            
            var boundaries = _field.GetBoundaries();
            var minY = boundaries.Value.Y;
            var maxY = boundaries.Value.Y + boundaries.Value.Height;
            
            if (_constructionGhost.Card.Scheme.IsDownEdge())
                maxY = boundaries.Value.Y + _constructionGhost.Card.Scheme.Placement.GetHeight(_constructionGhost.Rotation);

            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(_field, x, y);
                    
                    if (!(minY <= y && y < maxY))
                        continue;

                    if (occupiedSpace.Contains(fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }

            return new GroupOfPositions(list);
        }
    }
}