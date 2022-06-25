using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostCanBuildService : Disposable, IService
    {
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly IQuery<Construction> _constructions;
        private readonly ISingleQuery<Field> _field;

        public GhostCanBuildService(ISingleQuery<Field> field, 
            ISingleQuery<ConstructionGhost> ghost,
            IQuery<Construction> constructions)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            
            _ghost.OnAdded += UpdateState;
            _ghost.OnRemoved += UpdateState;
            _ghost.OnEvent += HandleEvent;

            _constructions.OnAdded += HandleConstructionChanged;
            _constructions.OnRemoved += HandleConstructionChanged;

            UpdateState();
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= UpdateState;
            _ghost.OnRemoved -= UpdateState;
            _ghost.OnEvent -= HandleEvent;
            
            _constructions.OnAdded -= HandleConstructionChanged;
            _constructions.OnRemoved -= HandleConstructionChanged;
            
            _field.Dispose();
            _constructions.Dispose();
            _ghost.Dispose();
        }

        public void UpdateState()
        {
            if (_ghost.Has())
            {
                var ghost = _ghost.Get();
                ghost.SetCanPlace(_constructions.CanPlace(ghost.Card.Scheme, ghost.Position, ghost.Rotation));
            }
        }

        private void HandleEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdateState();
        }
        
        private void HandleConstructionChanged(Construction obj)
        {
            UpdateState();
        }

    }
}
