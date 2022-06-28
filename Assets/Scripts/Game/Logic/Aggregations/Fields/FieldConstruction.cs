using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Fields
{
    public class FieldConstruction : Disposable, IAggregation
    {
        public Uid Id { get; }
        
        private readonly ConstructionEntity _entity;

        public FieldConstruction(ConstructionEntity entity)
        {
            _entity = entity;
        }
        
        public string GetViewPath()
        {
            return _entity.SchemeEntity.LevelViewPath;
        }
        
        public GameVector3 GetWorldPosition()
        {
            return _entity.GetWorldPosition();
        }

        public FieldRotation GetRotation()
        {
            return _entity.Rotation;
        }

        public bool IsItHaveSameScheme(FieldConstruction fieldConstruction)
        {
            return _entity.SchemeEntity == fieldConstruction._entity.SchemeEntity;
        }
        
        public bool IsItHaveSameScheme(ConstructionSchemeEntity construction)
        {
            return _entity.SchemeEntity == construction;
        }
    }
}