using System;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostModelPresenter : BasePresenter<IConstructionModelView>
    {
        private readonly IConstructionModelView _view;
        private readonly BuildingModeService _buildingModeService;
        private readonly ConstructionsService _constructionsService;

        public GhostModelPresenter(IConstructionModelView view) : this(view,
            IPresenterServices.Default?.Get<BuildingModeService>(),
            IPresenterServices.Default?.Get<ConstructionsService>()
            )
        {

        }

        public GhostModelPresenter(IConstructionModelView view, BuildingModeService buildingModeService, ConstructionsService constructionsService) : base(view)
        {
            _view = view ?? throw new System.ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new System.ArgumentNullException(nameof(buildingModeService));
            _constructionsService = constructionsService ?? throw new System.ArgumentNullException(nameof(constructionsService));

            if (!_buildingModeService.IsEnabled) throw new Exception("Ghost can exist only in building mode");

            _buildingModeService.OnPositionChanged += HandlePositionUpdate;
            _buildingModeService.OnChanged += HandleModeOnChanged;

            _view.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());

            HandlePositionUpdate();
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnPositionChanged -= HandlePositionUpdate;
            _buildingModeService.OnChanged -= HandleModeOnChanged;
        }

        private void HandleModeOnChanged(bool value)
        {
            if (!value) _view.Dispose();
        }

        private void HandlePositionUpdate()
        {
            var canPlace = _constructionsService.CanPlace(_buildingModeService.Card, 
                _buildingModeService.GetPosition(), _buildingModeService.GetRotation());

            _view.BorderAnimator.Play(canPlace ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
        }
    }
}
