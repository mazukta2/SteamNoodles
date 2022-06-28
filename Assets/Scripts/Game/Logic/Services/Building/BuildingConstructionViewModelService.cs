using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Services.Building
{
    public class BuildingConstructionViewModelService : Disposable, IService
    {
        private readonly ConstructionViewModelRepository _viewModels;
        private readonly GhostRepository _ghost;
        private readonly BuildingConstructionsRepository _constructions;

        public BuildingConstructionViewModelService(
            ConstructionViewModelRepository viewModels,
            GhostRepository ghostRepository,
            BuildingConstructionsRepository constructionsRepository)
        {
            _viewModels = viewModels ?? throw new ArgumentNullException(nameof(viewModels));
            _ghost = ghostRepository ?? throw new ArgumentNullException(nameof(ghostRepository));
            _constructions = constructionsRepository ?? throw new ArgumentNullException(nameof(constructionsRepository));
            _ghost.OnAdded += HandleOnAdded;
            _ghost.OnRemoved += HandleOnRemoved;
            _viewModels.OnFillRequest += UpdateShrink;
        }

        protected override void DisposeInner()
        {
            _ghost.OnAdded -= HandleOnAdded;
            _ghost.OnRemoved -= HandleOnRemoved;
            _viewModels.OnFillRequest -= UpdateShrink;
        }

        private void HandleOnAdded(BuildingGhost obj)
        {
            UpdateShrink();
            obj.OnMoved += UpdateShrink;
        }

        private void HandleOnRemoved(BuildingGhost obj)
        {
            UpdateShrink();
            obj.OnMoved -= UpdateShrink;
        }

        private void UpdateShrink()
        {
            foreach (var viewModel in _viewModels.Get())
                UpdateShrink(viewModel);
        }
        
        private void UpdateShrink(ConstructionViewModel viewModel)
        {
            var construction = _constructions.Get(viewModel.Id);
            viewModel.ChangeShrink(GetShrink(construction));
        }

        private float GetShrink(BuildingConstruction construction)
        {
            if (_ghost.Has())
            {
                var distance = _ghost.Get().GetTargetPosition().GetDistanceTo(construction.GetWorldPosition());
                if (distance > construction.GetGhostShrinkDistance())
                    return 1;
                else if (distance > construction.GetGhostHalfShrinkDistance())
                    return distance / construction.GetGhostShrinkDistance();
                else
                    return 0.2f;
            }
            else
            {
                return 1;
            }
        }
    }
}