using System;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class GhostModelPresenter : BasePresenter<IConstructionModelView>
    {
        private readonly IConstructionModelView _view;
        private readonly GhostService _ghostService;
        private readonly ConstructionsService _constructionsService;

        public GhostModelPresenter(IConstructionModelView view) : this(view,
            IPresenterServices.Default?.Get<GhostService>(),
            IPresenterServices.Default?.Get<ConstructionsService>()
            )
        {

        }

        public GhostModelPresenter(IConstructionModelView view, GhostService ghostService, ConstructionsService constructionsService) : base(view)
        {
            _view = view ?? throw new System.ArgumentNullException(nameof(view));
            _ghostService = ghostService ?? throw new System.ArgumentNullException(nameof(ghostService));
            _constructionsService = constructionsService ?? throw new System.ArgumentNullException(nameof(constructionsService));

            if (!_ghostService.IsEnabled()) throw new Exception("Ghost can exist only in building mode");

            _ghostService.OnPositionChanged += HandlePositionUpdate;
            _ghostService.OnChanged += HandleModeOnChanged;

            _view.Animator.Play(IConstructionModelView.Animations.Dragging.ToString());

            HandlePositionUpdate();
        }

        protected override void DisposeInner()
        {
            _ghostService.OnPositionChanged -= HandlePositionUpdate;
            _ghostService.OnChanged -= HandleModeOnChanged;
        }

        private void HandleModeOnChanged(bool value)
        {
            if (!value) _view.Dispose();
        }

        private void HandlePositionUpdate()
        {
            var canPlace = _constructionsService.CanPlace(_ghostService.GetCard(), 
                _ghostService.GetPosition(), _ghostService.GetRotation());

            _view.BorderAnimator.Play(canPlace ? IConstructionModelView.BorderAnimations.Idle.ToString() : IConstructionModelView.BorderAnimations.Disallowed.ToString());
        }
    }
}
