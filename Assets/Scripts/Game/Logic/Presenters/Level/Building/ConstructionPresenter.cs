using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private PresenterModel<Construction> _link;
        private IConstructionView _constructionView;
        private ConstructionsSettingsDefinition _constrcutionsSettings;
        private readonly IFieldPresenterService _fieldPositionService;
        private GhostManagerPresenter _ghostManager;
        private IControls _controls;
        private IConstructionModelView _modelView;
        private bool _dropFinished;
        private bool _isExploading;

        public ConstructionPresenter(ConstructionsSettingsDefinition constrcutionsSettings,
            EntityLink<Construction> construction, IFieldPresenterService fieldPositionService, IAssets assets, IConstructionView view,
            GhostManagerPresenter ghostManagerPresenter, IControls controls) : base(view)
        {
            _link = construction.CreateModel();
            _constructionView = view;
            _constrcutionsSettings = constrcutionsSettings;
            _fieldPositionService = fieldPositionService;
            _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
            _controls = controls;

            _constructionView.Position.Value = fieldPositionService.GetWorldPosition(_link.Get());
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(_link.Get().Rotation);

            _constructionView.Container.Clear();
            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(assets.GetPrefab(_link.Get().Scheme.Definition.LevelViewPath));
            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            _controls.ShakeCamera();

            _modelView.Animator.OnFinished += DropFinished;
            _link.OnDispose += HandleModelDispose;
            _link.OnEvent += HandleOnEvent;
            _ghostManager.OnGhostChanged += UpdateGhost;
            _ghostManager.OnGhostPostionChanged += UpdateGhost;
        }

        protected override void DisposeInner()
        {
            _link.Dispose();
            _link.OnDispose -= HandleModelDispose;
            _link.OnEvent -= HandleOnEvent;
            _modelView.Animator.OnFinished -= DropFinished;
            _ghostManager.OnGhostChanged -= UpdateGhost;
            _ghostManager.OnGhostPostionChanged -= UpdateGhost;
            _modelView.Animator.OnFinished -= ExplosionFinished;
        }

        public void UpdateGhost()
        {
            if (!_dropFinished)
                return;

            var ghost = _ghostManager.GetGhost();
            if (ghost != null)
            {
                var distance = ghost.GetTargetPosition().GetDistanceTo(_fieldPositionService.GetWorldPosition(_link.Get()));
                if (distance > _constrcutionsSettings.GhostShrinkDistance)
                    _modelView.Shrink.Value = 1;
                else if (distance > _constrcutionsSettings.GhostHalfShrinkDistance)
                    _modelView.Shrink.Value = distance / _constrcutionsSettings.GhostShrinkDistance;
                else
                    _modelView.Shrink.Value = 0.2f;
            }
            else
            {
                _modelView.Shrink.Value = 1;
            }
        }

        private void DropFinished()
        {
            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            _dropFinished = true;
            UpdateGhost();
        }

        private void ExplosionFinished()
        {
            _modelView.Animator.OnFinished -= ExplosionFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            _isExploading = false;
            if (_link.IsDisposed)
                _constructionView.Dispose();
        }

        private void HandleExplosion()
        {
            _modelView.Animator.OnFinished += ExplosionFinished;
            _isExploading = true;
            _modelView.Animator.Play(IConstructionModelView.Animations.Explode.ToString());
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _fieldPositionService.GetWorldPosition(_link.Get()));
        }

        private void HandleOnEvent(IModelEvent evt)
        {
            if (evt is ConstructionDestroyedOnWaveEndEvent)
                HandleExplosion();
        }


        private void HandleModelDispose()
        {
            if (_isExploading)
                return;

            _constructionView.Dispose();
        }

    }
}
