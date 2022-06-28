using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Constructions
{
    public class Construction : Disposable, IAggregation
    {
        public Uid Id { get; }
        
        private readonly ConstructionEntity _entity;
        private readonly GameAssetsService _assets;

        public Construction(ConstructionEntity entity, GameAssetsService assets)
        {
            _entity = entity;
            _assets = assets;
            // Scheme = scheme;
            // Rotation = FieldRotation.Default;
            // WorldPosition = GameVector3.Zero;
        }
        
        public IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_entity.SchemeEntity.LevelViewPath);
        }

        public GameVector3 GetWorldPosition()
        {
            return _entity.GetWorldPosition();
        }

        public FieldRotation GetRotation()
        {
            return _entity.Rotation;
        }

        public bool IsItHaveSameScheme(Construction construction)
        {
            return _entity.SchemeEntity == construction._entity.SchemeEntity;
        }
        
        public bool IsItHaveSameScheme(ConstructionSchemeEntity construction)
        {
            return _entity.SchemeEntity == construction;
        }
    }
}