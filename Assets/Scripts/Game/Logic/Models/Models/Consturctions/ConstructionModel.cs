using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class ConstructionModel : Disposable, IConstructionModel
    {
        public event Action OnDestroyed = delegate { };
        public event Action OnExplostion = delegate { };
        public event Action OnUpdate = delegate { };

        public ConstructionScheme Scheme => _construction.Scheme;

        public FieldRotation GetRotation()
        {
            return _construction.Rotation;
        }

        public GameVector3 GetWorldPosition()
        {
            return _service.GetWorldPosition(_construction);
        }

        public float GhostShrinkDistance => Scheme.GhostShrinkDistance;
        public float GhostHalfShrinkDistance => Scheme.GhostHalfShrinkDistance;

        private readonly Construction _construction;
        private readonly ConstructionsRequestsService _service;
        private readonly IAssets _assets;

        public ConstructionModel(Construction construction, ConstructionsRequestsService service, IAssets assets)
        {
            _construction = construction;
            _service = service;
            _assets = assets;
            _service.OnUpdate += HandleOnUpdate;
            _service.OnRemoved += HandleOnRemoved;
        }

        protected override void DisposeInner()
        {
            _service.OnUpdate -= HandleOnUpdate;
            _service.OnRemoved -= HandleOnRemoved;
        }

        private void HandleOnUpdate()
        {
            OnUpdate();
        }

        public IViewPrefab GetModelAsset()
        {
            return _assets.GetPrefab(Scheme.LevelViewPath);
        }

        public void Shake()
        {
            _service.Shake();
        }

        public float GetShrinkValue()
        {
            return _service.GetShrink(this);
        }

        private void HandleOnRemoved(Construction obj)
        {
            if (_construction.Id == obj.Id)
                OnExplostion();
        }

    }
}
