using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.DataObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Services.Fields
{
    public class BuildingAggregatorService : Disposable, IService, 
        IDataProviderService<GhostData>, IDataProviderService<FieldData>
    {
        IDataProvider<GhostData> IDataProviderService<GhostData>.Get() => Ghost;
        IDataProvider<FieldData> IDataProviderService<FieldData>.Get() => Field;
        
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly IRepository<Construction> _constructions;
        private readonly ISingletonRepository<Field> _field;

        public DataProvider<FieldData> Field { get; }= new DataProvider<FieldData>();
        public DataProvider<GhostData> Ghost { get; }= new DataProvider<GhostData>();
        
        public BuildingAggregatorService(ISingletonRepository<Field> field, 
            ISingletonRepository<ConstructionGhost> ghost,
            IRepository<Construction> constructions)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            
            _ghost.OnAdded += UpdateCells;
            _ghost.OnRemoved += UpdateCells;
            _ghost.OnEvent += HandleEvent;

            _constructions.OnAdded += HandleConstructionChanged;
            _constructions.OnRemoved += HandleConstructionChanged;

            Field.Add(new FieldData());

            UpdateCells();
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= UpdateCells;
            _ghost.OnRemoved -= UpdateCells;
            _ghost.OnEvent -= HandleEvent;
            
            _constructions.OnAdded -= HandleConstructionChanged;
            _constructions.OnRemoved -= HandleConstructionChanged;
        }

        private void UpdateCells()
        {
            ConstructionGhost ghost = null;
            if (_ghost.Has())
            {
                ghost = _ghost.Get();
                var cells = GetAvailableToBuildCells( ghost.Card.Scheme, ghost.Rotation);
                var field = Field.Get();
                field.AvailableCells = cells;
                
                
                var ghostData = Ghost.Get() ?? new GhostData();
                ghostData.CanBuild = CanPlace(ghost.Card.Scheme, ghost.Position, ghost.Rotation);
                ghostData.Points = GetPoints(ghost.Card.Scheme, ghost.Position, ghost.Rotation);
                if (!Ghost.Has()) Ghost.Add(ghostData);
            }
            else
            {
                var cells = GetUnoccupiedCells();
                var field = Field.Get();
                field.AvailableCells = cells;
                Ghost.Remove();
            }
            
            
            // var boundaries = _field.Get().Boundaries;
            // for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            // {
            //     for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
            //     {
            //         _cells.Add(CreateCell(new FieldPosition(_field.Get(), x, y)));
            //     }
            // }
        }

        private void HandleEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdateCells();
        }
        
        private void HandleConstructionChanged(Construction obj)
        {
            UpdateCells();
        }

        private bool CanPlace(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            var occupiedCells = scheme.Placement.GetOccupiedSpace(position, rotation);
            return occupiedCells.All(occupiedPosition => Field.Get().AvailableCells.Cells.Contains(occupiedPosition));
        }

        private GroupOfPositions GetAvailableToBuildCells(ConstructionScheme scheme, FieldRotation rotation)
        {
            var list = new HashSet<FieldPosition>();
            var occupiedSpace = _constructions.Get()
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace());
            
            var boundaries = _field.Get().GetBoundaries();
            var minY = boundaries.Value.Y;
            var maxY = boundaries.Value.Y + boundaries.Value.Height;
            
            if (scheme.IsDownEdge())
                maxY = boundaries.Value.Y + scheme.Placement.GetHeight(rotation);

            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(_field.Get(), x, y);
                    
                    if (!(minY <= y && y < maxY))
                        continue;

                    if (occupiedSpace.Contains(fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }

            return new GroupOfPositions(list);
        }

        private GroupOfPositions GetUnoccupiedCells()
        {
            var list = new List<FieldPosition>();
            
            var constructionsList = _constructions.Get();
            var occupiedSpace = constructionsList
                .SelectMany(otherBuilding => otherBuilding.GetOccupiedSpace());
            
            var boundaries = _field.Get().GetBoundaries();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var fieldPosition = new FieldPosition(_field.Get(), x, y);
                    if (occupiedSpace.Any(pos => pos == fieldPosition))
                        continue;
                    
                    list.Add(fieldPosition);
                }
            }
            
            return new GroupOfPositions(list);
        }
        
        private bool IsAvailableToBuild(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
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

        private BuildingPoints GetPoints(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation)
        {
            if (!Ghost.Get().CanBuild)
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
    }
}
