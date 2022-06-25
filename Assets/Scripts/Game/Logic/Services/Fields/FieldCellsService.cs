using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Services.Fields
{
    public class FieldCellsService : Disposable, IService
    {
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly IQuery<Construction> _constructions;
        private readonly ISingleQuery<Field> _field;

        public FieldCellsService(ISingleQuery<Field> field, 
            ISingleQuery<ConstructionGhost> ghost,
            IQuery<Construction> constructions)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));
            
            _ghost.OnAdded += UpdateCells;
            _ghost.OnRemoved += UpdateCells;
            _ghost.OnEvent += HandleEvent;

            _constructions.OnAdded += HandleConstructionChanged;
            _constructions.OnRemoved += HandleConstructionChanged;

            UpdateCells();
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= UpdateCells;
            _ghost.OnRemoved -= UpdateCells;
            _ghost.OnEvent -= HandleEvent;
            
            _constructions.OnAdded -= HandleConstructionChanged;
            _constructions.OnRemoved -= HandleConstructionChanged;
            
            _field.Dispose();
            _constructions.Dispose();
            _ghost.Dispose();
        }

        public void UpdateCells()
        {
            var field = _field.Get();
            
            ConstructionGhost ghost = null;
            if (_ghost.Has())
            {
                ghost = _ghost.Get();
                var cells = _constructions.GetAvailableToBuildCells(_field.Get(), ghost.Card.Scheme, ghost.Rotation);
                field.SetAvailableCells(cells);
            }
            else
            {
                var cells = _constructions.GetUnoccupiedCells(_field.Get());
                field.SetAvailableCells(cells);
            }
        }

        private void HandleEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent)
                return;
            
            UpdateCells();
        }
        
        private void HandleConstructionChanged(Construction obj)
        {
            UpdateCells();
        }

    }
}
