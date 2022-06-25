using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostPointsService : Disposable, IService
    {
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly IQuery<Construction> _constructions;

        public GhostPointsService(ISingletonRepository<ConstructionGhost> ghost, IQuery<Construction> constructions)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _ghost.OnAdded += HandleOnAdded;
            _ghost.OnEvent += HandleOnMoved;
            _constructions.OnAdded += HandleConstructionChanged;
            _constructions.OnRemoved += HandleConstructionChanged;
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= HandleOnAdded;
            _ghost.OnEvent -= HandleOnMoved;
            _constructions.OnAdded -= HandleConstructionChanged;
            _constructions.OnRemoved -= HandleConstructionChanged;
            _constructions.Dispose();
        }

        private void HandleOnAdded()
        {
            HandleUpdate();
        }

        private void HandleOnMoved(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            HandleUpdate();
        }

        private void HandleUpdate()
        {
            var ghost = _ghost.Get();
            ghost.SetPoints(_constructions.GetPoints(ghost.Card.Scheme, ghost.Position, ghost.Rotation));
        }
        
        private void HandleConstructionChanged(Construction obj)
        {
            HandleUpdate();
        }

    }
}
