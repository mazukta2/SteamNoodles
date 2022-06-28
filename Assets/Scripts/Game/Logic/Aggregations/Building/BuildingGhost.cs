using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Building
{
    public class BuildingGhost : Disposable, IAggregation
    {
        public event Action OnMoved = delegate { };
        
        public Uid Id => _ghostEntity.Id;
        
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly FieldEntity _fieldEntity;
        private readonly GhostEntity _ghostEntity;

        public BuildingGhost(GhostEntity ghostEntity, IDatabase<ConstructionEntity> constructions, FieldEntity fieldEntity)
        {
            _ghostEntity = ghostEntity;
            _constructions = constructions;
            _fieldEntity = fieldEntity;

            _ghostEntity.OnMoved += HandleMoved;
            _ghostEntity.OnRotated += HandleRotated;
        }


        protected override void DisposeInner()
        {
            _ghostEntity.OnMoved -= HandleMoved;
            _ghostEntity.OnRotated -= HandleRotated;
        }

        public ConstructionEntity Build()
        {
            if (!CanBuild())
                throw new Exception("Can't build construction here");

            var construction = new ConstructionEntity(_ghostEntity.CardEntity.SchemeEntity, 
                GetPosition(), 
                _ghostEntity.Rotation);
            var points = GetPoints();

            _constructions.Add(construction);

            _constructions.FireEvent(construction, new ConstructionBuiltByPlayerEvent(points, _ghostEntity.CardEntity));

            return construction;
        }
        
        public void TryBuild()
        {
            if (CanBuild())
                Build();
        }

        public bool CanBuild()
        {
            var occupiedCells = GetOccupiedSpace().Cells;
            return occupiedCells.All(occupiedPosition => GetAvailableToBuildCells().Cells.Contains(occupiedPosition));
        }

        public FieldRotation GetRotation()
        {
            return _ghostEntity.Rotation;
        }

        public BuildingPoints GetPoints()
        {
            if (!CanBuild())
                return new BuildingPoints(0);

            var adjacentPoints = BuildingPoints.Zero;
            foreach (var construction in GetAdjacentConstructions(_ghostEntity.CardEntity.SchemeEntity, 
                         GetPosition(), _ghostEntity.Rotation))
            {
                if (_ghostEntity.CardEntity.SchemeEntity.AdjacencyPoints.HasAdjacencyBonusWith(construction.SchemeEntity))
                {
                    adjacentPoints += _ghostEntity.CardEntity.SchemeEntity.AdjacencyPoints.GetAdjacencyBonusWith(construction.SchemeEntity);
                }
            }

            return _ghostEntity.CardEntity.SchemeEntity.Points + adjacentPoints;
        }

        public void SetPosition(GameVector3 pointerPosition)
        {
            _ghostEntity.SetPosition(pointerPosition);
        }

        public void SetRotation(FieldRotation rotation)
        {
            _ghostEntity.SetRotation(rotation);
        }
        
        public GroupOfPositions GetOccupiedSpace()
        {
            return new GroupOfPositions(_ghostEntity.CardEntity.SchemeEntity.Placement
                .GetOccupiedSpace(GetPosition(), _ghostEntity.Rotation));
        }

        public GameVector3 GetWorldPosition()
        {
            return GetPosition().GetWorldPosition();
        }

        public GameVector3 GetTargetPosition()
        {
            return _ghostEntity.TargetPosition;
        }

        public FieldPosition GetPosition()
        {
            return GetPosition(_ghostEntity.TargetPosition);
        }
        
        
        private FieldPosition GetPosition(GameVector3 target)
        {
            var size = _ghostEntity.CardEntity.SchemeEntity.Placement.GetRect(_ghostEntity.Rotation);
            return _fieldEntity.GetFieldPosition(target, size);
        }

        private IReadOnlyCollection<ConstructionEntity> GetAdjacentConstructions( 
            ConstructionSchemeEntity schemeEntity,
            FieldPosition position, FieldRotation rotation)
        {
            var adjacentCells = GetListOfAdjacentCells(schemeEntity, position, rotation);
            var adjacentConstructions = new List<ConstructionEntity>();
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
        
        
        private static IReadOnlyCollection<FieldPosition> GetListOfAdjacentCells(ConstructionSchemeEntity schemeEntity, FieldPosition position, FieldRotation rotation)
        {
            var list = new List<FieldPosition>();

            foreach (var cell in schemeEntity.Placement.GetOccupiedSpace(position, rotation))
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
        
        public GroupOfPositions GetAvailableToBuildCells()
        {
            var list = new HashSet<FieldPosition>();
            var occupiedSpace = _constructions.Get()
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace()).AsReadOnly();
            
            var boundaries = _fieldEntity.GetBoundaries();
            var minY = boundaries.Value.Y;
            var maxY = boundaries.Value.Y + boundaries.Value.Height;
            
            if (_ghostEntity.CardEntity.SchemeEntity.IsDownEdge())
                maxY = boundaries.Value.Y + _ghostEntity.CardEntity.SchemeEntity.Placement.GetHeight(_ghostEntity.Rotation);

            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(_fieldEntity, x, y);
                    
                    if (!(minY <= y && y < maxY))
                        continue;

                    if (occupiedSpace.Contains(fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }

            return new GroupOfPositions(list);
        }
        
        private void HandleRotated()
        {
            OnMoved();
        }

        private void HandleMoved(GameVector3 oldPos, GameVector3 newPos)
        {
            if (GetPosition(oldPos) != GetPosition(newPos))
                OnMoved();
        }

        public string GetViewPath()
        {
            return _ghostEntity.CardEntity.SchemeEntity.LevelViewPath;
        }
    }
}