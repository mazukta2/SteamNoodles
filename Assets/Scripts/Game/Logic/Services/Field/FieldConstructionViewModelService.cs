using System;
using Game.Assets.Scripts.Game.Logic.Aggregations.Fields;
using Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Constructions;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Services.Assets;

namespace Game.Assets.Scripts.Game.Logic.Services.Field
{
    public class FieldConstructionViewModelService : Disposable, IService
    {
        private readonly ConstructionViewModelRepository _viewModels;
        private readonly ConstructionsRepository _constructions;
        private readonly GameAssetsService _assets;

        public FieldConstructionViewModelService(
            ConstructionViewModelRepository viewModels,
            ConstructionsRepository constructions, GameAssetsService assets)
        {
            _viewModels = viewModels ?? throw new ArgumentNullException(nameof(viewModels));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _viewModels.OnFillRequest += HandleOnFill;
        }

        protected override void DisposeInner()
        {
            _viewModels.OnFillRequest -= HandleOnFill;
        }

        private void HandleOnFill(ConstructionViewModel viewModel)
        {
            var construction = _constructions.Get(viewModel.Id);
            viewModel.WorldPosition = construction.GetWorldPosition();
            viewModel.Rotation = construction.GetRotation();
            viewModel.Prefab = _assets.GetPrefab(construction.GetViewPath());
        }
    }
}