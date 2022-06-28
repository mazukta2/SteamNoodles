using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
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
    public class Ghost : Disposable, IAggregation
    {
        public Uid Id => _ghostEntity.Id;
        
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly FieldEntity _fieldEntity;
        private readonly GhostEntity _ghostEntity;

        public Ghost(GhostEntity ghostEntity, IDatabase<ConstructionEntity> constructions, FieldEntity fieldEntity)
        {
            _constructions = constructions;
            _fieldEntity = fieldEntity;
            _ghostEntity = ghostEntity;
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
            // if (Position != cellPosition)
            // {
            //     Position = cellPosition;
            //     FireEvent(new GhostMovedEvent());
            // }
        }

        public void SetRotation(FieldRotation rotation)
        {
            _ghostEntity.SetRotation(rotation);
        }
        // public bool CanBuild { get; private set; }
        // public BuildingPoints Points { get; private set; }
        // public FieldRotation Rotation { get; private set; }
        // public FieldPosition Position { get;  private set; }
        // public GameVector3 TargetPosition { get; private set; }
        // public GameVector3 WorldPosition { get; private set; }
        // public IDataProvider<ConstructionCardData> CardData { get; private set; }
        // public IViewPrefab Prefab { get; set; }
        // public IReadOnlyCollection<FieldPosition> OccupiedSpace { get; private set; }
        // // public ConstructionCard Card { get; set; }
        //
        // private Field _field;
        // private ContructionPlacement _placement;
        //
        // public static GhostData Default(Field field)
        // {
        //     var data = new GhostData();
        //     data._field = field;
        //     data._placement = ContructionPlacement.One;
        //     data.Points = new BuildingPoints(0);
        //     data.Position = new FieldPosition(field,0, 0);
        //     data.Rotation = new FieldRotation();
        //     data.TargetPosition = GameVector3.Zero;
        //     data.CardData = new DataProvider<ConstructionCardData>(ConstructionCardData.Default());
        //     data.OccupiedSpace = data._placement.GetOccupiedSpace(data.Position, data.Rotation);
        //     return data;
        // }
        //
        // public GhostData SetPosition(GameVector3 targetPosition)
        // {
        //     Position =_field.GetFieldPosition(targetPosition, CardData.Get().Size);
        //     TargetPosition = targetPosition;
        //     WorldPosition = Position.GetWorldPosition(CardData.Get().Size);
        //     OccupiedSpace = _placement.GetOccupiedSpace(Position, Rotation);
        //     return this;
        // }
        
        // public IReadOnlyCollection<FieldPosition> GetOccupiedSpace()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public BuildingPoints GetPoints()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public GameVector3 GetWorldPosition()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public GameVector3 GetTargetPosition()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public IViewPrefab GetPrefab()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public FieldRotation GetRotation()
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public bool GetCanBuild()
        // {
        //     throw new NotImplementedException();
        // }

        public IViewPrefab GetPrefab()
        {
            throw new NotImplementedException();
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
            var size = _ghostEntity.CardEntity.SchemeEntity.Placement.GetRect(_ghostEntity.Rotation);
            return _fieldEntity.GetFieldPosition(_ghostEntity.TargetPosition, size);
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
    }
}