using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostMovingService : Disposable, IService
    {
        private readonly GhostService _ghostService;
        private readonly Field _field;
        private readonly GameControlsService _controlsService;

        public GhostMovingService(GhostService ghostService, Field field, GameControlsService controlsService)
        {
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelPointerMoved += HandleOnOnLevelPointerMoved;
            _ghostService.OnShowed += HandleOnShowed;
        }

        protected override void DisposeInner()
        {
            _ghostService.OnShowed -= HandleOnShowed;
            _controlsService.OnLevelPointerMoved -= HandleOnOnLevelPointerMoved;
        }

        public void SetTargetPosition(GameVector3 pointerPosition)
        {       
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            var fieldPosition = _field.GetFieldPosition(pointerPosition, size);
            ghost.SetPosition(fieldPosition, pointerPosition);
            _ghostService.Set(ghost);
        }

        public void SetTargetPosition(FieldPosition cellPosition)
        {
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            ghost.SetPosition(cellPosition, cellPosition.GetWorldPosition(size));
            _ghostService.Set(ghost);
        }

        private void HandleOnOnLevelPointerMoved(GameVector3 target)
        {
            if (!_ghostService.IsEnabled())
                return;

            SetTargetPosition(target);
        }
        
        private void HandleOnShowed()
        {
            SetTargetPosition(_controlsService.PointerLevelPosition);
        }
    }
}
