using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public class GhostMovingService : Disposable, IService
    {
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly ISingletonRepository<Field> _field;
        private readonly GameControlsService _controlsService;

        public GhostMovingService(ISingletonRepository<ConstructionGhost> ghost, ISingletonRepository<Field> field, GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelPointerMoved += HandleOnOnLevelPointerMoved;
            _ghost.OnAdded += HandleOnShowed;
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= HandleOnShowed;
            _controlsService.OnLevelPointerMoved -= HandleOnOnLevelPointerMoved;
        }

        public void SetTargetPosition(GameVector3 pointerPosition)
        {
            if (!_ghost.Has())
                throw new Exception("No available ghost");
            
            var ghost = _ghost.Get();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            var fieldPosition = _field.Get().GetFieldPosition(pointerPosition, size);
            ghost.SetPosition(fieldPosition, pointerPosition);
        }

        public void SetTargetPosition(FieldPosition cellPosition)
        {
            if (!_ghost.Has())
                throw new Exception("No available ghost");
            
            var ghost = _ghost.Get();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            ghost.SetPosition(cellPosition, cellPosition.GetWorldPosition(size));
        }

        private void HandleOnOnLevelPointerMoved(GameVector3 target)
        {
            if (!_ghost.Has())
                return;

            SetTargetPosition(target);
        }
        
        private void HandleOnShowed()
        {
            SetTargetPosition(_controlsService.PointerLevelPosition);
        }
    }
}
