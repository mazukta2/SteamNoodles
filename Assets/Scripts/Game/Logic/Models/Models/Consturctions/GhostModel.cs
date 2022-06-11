using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class GhostModel : Disposable, IGhostModel
    {
        private GhostRequestsService _requestsService;

        public bool IsActive => throw new NotImplementedException();

        public event Action OnUpdate = delegate { };

        public GhostModel(GhostRequestsService requestsService)
        {
            _requestsService = requestsService;
            _requestsService.OnUpdate += HandleOnUpdate;
        }

        protected override void DisposeInner()
        {
            _requestsService.OnUpdate -= HandleOnUpdate;
        }

        private void HandleOnUpdate()
        {
            OnUpdate();
        }

        public GameVector3 WorldPosition()
        {
            throw new NotImplementedException();
        }

        public GameVector3 GetTargetPosition()
        {
            throw new NotImplementedException();
        }
    }
}
