using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Building;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;

namespace Game.Assets.Scripts.Game.Logic.Services.Building
{
    public class BuildingGhostViewModelService : Disposable, IService
    {
        private readonly GhostViewModelRepository _viewModels;
        private readonly GhostRepository _ghostRepository;
        private readonly GameAssetsService _assetsService;

        public BuildingGhostViewModelService(
            GhostViewModelRepository viewModels,
            GhostRepository ghostRepository,
            GameAssetsService assetsService)
        {
            _viewModels = viewModels ?? throw new ArgumentNullException(nameof(viewModels));
            _ghostRepository = ghostRepository ?? throw new ArgumentNullException(nameof(ghostRepository));
            _assetsService = assetsService ?? throw new ArgumentNullException(nameof(assetsService));
            _viewModels.OnFillRequest += Fill;
            _ghostRepository.OnAdded += HandleOnAdded;
            _ghostRepository.OnRemoved += HandleOnRemoved;
        }

        protected override void DisposeInner()
        {
            _viewModels.OnFillRequest -= Fill;
        }

        private void Fill(GhostViewModel viewModel)
        {
            var ghost = _ghostRepository.Get();
            
            viewModel.Prefab = _assetsService.GetPrefab(ghost.GetViewPath());
            viewModel.Rotation = ghost.GetRotation();
            viewModel.WorldPosition = ghost.GetWorldPosition();
            viewModel.CanBuild = ghost.CanBuild();
        }
        
        private void HandleOnAdded(BuildingGhost ghost)
        {
            ghost.OnMoved += HandleOnMoved;
        }

        private void HandleOnRemoved(BuildingGhost ghost)
        {
            ghost.OnMoved -= HandleOnMoved;
        }

        private void HandleOnMoved()
        {
            Fill(_viewModels.Get());
        }
    }
}