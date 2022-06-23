using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostMovingService : Disposable, IService
    {
        private readonly GhostService _ghostService;
        private readonly Field _field;
        private readonly GameControlsService _controlsService;
        private IReadOnlyCollection<Construction> _constructionsHighlights = new List<Construction>();

        public GhostMovingService(GhostService ghostService, Field field, GameControlsService controlsService)
        {
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelPointerMoved += HandleOnOnLevelPointerMoved;
            
            //    _rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
            //    _rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

            //_rotateLeft.OnTap += HandleRotateLeftTap;
            //_rotateRight.OnTap += HandleRotateRightTap;
            //_rotateLeft = _gameKeysManager.GetKey(GameKeys.RotateLeft);
            //_rotateRight = _gameKeysManager.GetKey(GameKeys.RotateRight);

            _ghostService.OnShowed += HandleOnShowed;
        }

        protected override void DisposeInner()
        {
            _ghostService.OnShowed -= HandleOnShowed;
            _controlsService.OnLevelPointerMoved -= HandleOnOnLevelPointerMoved;
        }

        public IReadOnlyCollection<Construction> GetConstructionsHighlights()
        {
            return _constructionsHighlights;
        }

        public void SetHighlight(IReadOnlyCollection<Construction> constructions)
        {
            _constructionsHighlights = constructions;
            // OnHighlightingChanged();
        }

        public void SetTargetPosition(GameVector3 pointerPosition)
        {       
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            var fieldPosition = _field.GetFieldPosition(pointerPosition, size);
            ghost.SetPosition(fieldPosition, pointerPosition);
            _ghostService.Set(ghost);
        }

        public void SetTargetPosition(CellPosition cellPosition)
        {
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            ghost.SetPosition(cellPosition, _field.GetWorldPosition(cellPosition, size));
            _ghostService.Set(ghost);
        }

        public void SetRotation(FieldRotation rotation)
        {
            var ghost = _ghostService.GetGhost();
            ghost.SetRotation(rotation);
            _ghostService.Set(ghost);
        }

        private void HandleOnOnLevelPointerMoved(GameVector3 target)
        {
            if (!_ghostService.IsEnabled())
                return;

            SetTargetPosition(target);
        }
        
        //private void HandleRotateLeftTap()
        //{
        //    //Rotation = FieldRotation.RotateLeft(Rotation);
        //    //UpdatePosition();
        //}

        //private void HandleRotateRightTap()
        //{
        //    //Rotation = FieldRotation.RotateRight(Rotation);
        //    //UpdatePosition();
        //}
        
        private void HandleOnShowed()
        {
            SetTargetPosition(_controlsService.PointerLevelPosition);
        }
    }
}
