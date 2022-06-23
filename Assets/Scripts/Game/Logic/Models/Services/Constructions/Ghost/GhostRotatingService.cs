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
    public class GhostRotatingService : Disposable, IService
    {
        private readonly GhostService _ghostService;
        private readonly KeyCommand _rotateLeft;
        private readonly KeyCommand _rotateRight;

        public GhostRotatingService(GhostService ghostService, GameControlsService controlsService)
        {
            _ghostService = ghostService ?? throw new ArgumentNullException(nameof(ghostService));
            
            _rotateLeft = controlsService.GetKey(GameKeys.RotateLeft);
            _rotateRight = controlsService.GetKey(GameKeys.RotateRight);

            _rotateLeft.OnTap += HandleRotateLeftTap;
            _rotateRight.OnTap += HandleRotateRightTap;
        }

        protected override void DisposeInner()
        {
            _rotateLeft.OnTap -= HandleRotateLeftTap;
            _rotateRight.OnTap -= HandleRotateRightTap;
        }

        public void SetRotation(FieldRotation rotation)
        {
            var ghost = _ghostService.GetGhost();
            ghost.SetRotation(rotation);
            _ghostService.Set(ghost);
        }

        private void HandleRotateLeftTap()
        {
            if (!_ghostService.IsEnabled())
                return;
            
            var ghost = _ghostService.GetGhost();
            SetRotation(FieldRotation.RotateLeft(ghost.Rotation));
        }

        private void HandleRotateRightTap()
        {
            if (!_ghostService.IsEnabled())
                return;
            
            var ghost = _ghostService.GetGhost();
            SetRotation(FieldRotation.RotateRight(ghost.Rotation));
        }
    }
}
