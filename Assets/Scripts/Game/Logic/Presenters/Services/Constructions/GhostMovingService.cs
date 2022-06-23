using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions
{
    public class GhostMovingService : Disposable, IService
    {
        private readonly GhostService _ghostService;
        private readonly FieldService _fieldService;
        private readonly GameControlsService _controlsService;
        private IReadOnlyCollection<Construction> _constructionsHighlights = new List<Construction>();

        public GhostMovingService() : this(
            IPresenterServices.Default.Get<GhostService>(),
            IPresenterServices.Default.Get<FieldService>(),
            IPresenterServices.Default.Get<GameControlsService>())
        {
        }
        
        public GhostMovingService(GhostService ghostService, FieldService fieldService, GameControlsService controlsService)
        {
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
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
            var fieldPosition = _fieldService.GetFieldPosition(pointerPosition, size);
            ghost.SetPosition(fieldPosition, pointerPosition);
            _ghostService.Set(ghost);
        }

        public void SetTargetPosition(FieldPosition fieldPosition)
        {
            var ghost = _ghostService.GetGhost();
            var size = ghost.Card.Scheme.Placement.GetRect(ghost.Rotation);
            ghost.SetPosition(fieldPosition, _fieldService.GetWorldPosition(fieldPosition, size));
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
