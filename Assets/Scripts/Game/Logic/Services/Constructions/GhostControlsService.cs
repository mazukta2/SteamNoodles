using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class GhostControlsService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly GhostRepository _ghostRepository;
        private readonly KeyCommand _rotateRight;
        private readonly KeyCommand _rotateLeft;

        public GhostControlsService(
            GhostRepository ghostRepository,
            GameControlsService controlsService)
        {
            _ghostRepository = ghostRepository ?? throw new ArgumentNullException(nameof(ghostRepository));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _rotateLeft = controlsService.GetKey(GameKeys.RotateLeft);
            _rotateRight = controlsService.GetKey(GameKeys.RotateRight);
            
            _controlsService.OnLevelClick += HandleOnLevelClick;
            _controlsService.OnLevelPointerMoved += HandleOnOnLevelPointerMoved;
            _ghostRepository.OnAdded += HandleOnShowed;
            _rotateLeft.OnTap += HandleRotateLeftTap;
            _rotateRight.OnTap += HandleRotateRightTap;
        }

        protected override void DisposeInner()
        {
            _controlsService.OnLevelClick -= HandleOnLevelClick;
            _ghostRepository.OnAdded -= HandleOnShowed;
            _controlsService.OnLevelPointerMoved -= HandleOnOnLevelPointerMoved;
            _rotateLeft.OnTap -= HandleRotateLeftTap;
            _rotateRight.OnTap -= HandleRotateRightTap;
        }

        private void HandleOnLevelClick()
        {
            if (!_ghostRepository.Has())
                return;
            
            _ghostRepository.Get().TryBuild();
            _ghostRepository.Remove();
        }
        
        private void SetTargetPosition(GameVector3 pointerPosition)
        {
            if (!_ghostRepository.Has())
                throw new Exception("No available ghost");
            
            _ghostRepository.Get().SetPosition(pointerPosition);
        }

        private void HandleOnOnLevelPointerMoved(GameVector3 target)
        {
            if (!_ghostRepository.Has())
                return;

            SetTargetPosition(target);
        }
        
        private void HandleOnShowed(Ghost placing)
        {
            SetTargetPosition(_controlsService.PointerLevelPosition);
        }
        
        private void SetRotation(FieldRotation rotation)
        {
            if (!_ghostRepository.Has())
                throw new Exception("No ghost available");
            
            _ghostRepository.Get().SetRotation(rotation);
        }

        private void HandleRotateLeftTap()
        {
            if (!_ghostRepository.Has())
                return;
            
            var ghost = _ghostRepository.Get();
            SetRotation(FieldRotation.RotateLeft(ghost.GetRotation()));
        }

        private void HandleRotateRightTap()
        {
            if (!_ghostRepository.Has())
                return;
            
            var ghost = _ghostRepository.Get();
            SetRotation(FieldRotation.RotateRight(ghost.GetRotation()));
        }
    }
}
