using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionShrinkerPresenter : BasePresenter<IConstructionModelView>
    {
        private readonly IConstructionModelView _constructionView;
        private Construction _construction;
        private readonly FieldService _fieldPositionService;
        private readonly BuildingModeService _buildingModeService;

        public ConstructionShrinkerPresenter(IConstructionModelView view, Construction construction) 
            : this(view, construction,
                  IPresenterServices.Default?.Get<FieldService>(),
                  IPresenterServices.Default?.Get<BuildingModeService>())
        {

        }

        public ConstructionShrinkerPresenter(IConstructionModelView view,
            Construction construction,
            FieldService fieldPositionService,
            BuildingModeService buildingModeService) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _fieldPositionService = fieldPositionService ?? throw new ArgumentNullException(nameof(fieldPositionService));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));

            _constructionView.Animator.OnFinished += AnimtationFinished;
            _buildingModeService.OnChanged += HandleOnChanged;
            _buildingModeService.OnPositionChanged += HandleOnPositionChanged;

            UpdateShrink();
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnChanged -= HandleOnChanged;
            _buildingModeService.OnPositionChanged -= HandleOnPositionChanged;
            _constructionView.Animator.OnFinished -= AnimtationFinished;
        }

        private void UpdateShrink()
        {
            if (!IsIdle())
                return;

            if (_buildingModeService.IsEnabled)
            {
                var distance = _buildingModeService.GetTargetPosition()
                    .GetDistanceTo(_fieldPositionService.GetWorldPosition(_construction));
                if (distance > _construction.Scheme.GhostShrinkDistance)
                    _constructionView.Shrink.Value = 1;
                else if (distance > _construction.Scheme.GhostHalfShrinkDistance)
                    _constructionView.Shrink.Value = distance / _construction.Scheme.GhostShrinkDistance;
                else
                    _constructionView.Shrink.Value = 0.2f;
            }
            else
            {
                _constructionView.Shrink.Value = 1;
            }
        }

        private bool IsIdle()
        {
            var currentAnimation = _constructionView.Animator.GetCurrentAnimation();
            if (string.IsNullOrEmpty(currentAnimation))
                return true;

            if (currentAnimation == IConstructionModelView.Animations.Idle.ToString())
                return true;

            return false;
        }

        private void AnimtationFinished()
        {
            UpdateShrink();
        }

        private void HandleOnPositionChanged()
        {
            UpdateShrink();
        }

        private void HandleOnChanged(bool obj)
        {
            UpdateShrink();
        }
    }
}
