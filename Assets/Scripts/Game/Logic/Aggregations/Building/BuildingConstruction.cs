using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Building
{
    public class BuildingConstruction : Disposable, IAggregation
    {
        public Uid Id { get; }
        
        private readonly ConstructionEntity _entity;

        public BuildingConstruction(ConstructionEntity entity)
        {
            _entity = entity;
        }
        
        public float GetGhostShrinkDistance()
        {
            return _entity.SchemeEntity.GhostShrinkDistance;
        }

        public float GetGhostHalfShrinkDistance()
        {
            return _entity.SchemeEntity.GhostHalfShrinkDistance;
        }

        public GameVector3 GetWorldPosition()
        {
            return _entity.GetWorldPosition();
        }
        
    }
}