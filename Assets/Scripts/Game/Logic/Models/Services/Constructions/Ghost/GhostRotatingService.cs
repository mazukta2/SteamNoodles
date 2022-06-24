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
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly KeyCommand _rotateLeft;
        private readonly KeyCommand _rotateRight;

        public GhostRotatingService(ISingletonRepository<ConstructionGhost> ghost, GameControlsService controlsService)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            
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
            if (!_ghost.Has())
                throw new Exception("No ghost available");
            
            var ghost = _ghost.Get();
            ghost.SetRotation(rotation);
            _ghost.Save(ghost);
        }

        private void HandleRotateLeftTap()
        {
            if (!_ghost.Has())
                return;
            
            var ghost = _ghost.Get();
            SetRotation(FieldRotation.RotateLeft(ghost.Rotation));
        }

        private void HandleRotateRightTap()
        {
            if (!_ghost.Has())
                return;
            
            var ghost = _ghost.Get();
            SetRotation(FieldRotation.RotateRight(ghost.Rotation));
        }
    }
}
