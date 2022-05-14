using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private Construction _construction;
        private IConstructionView _constructionView;
        private ConstructionsSettingsDefinition _constrcutionsSettings;
        private GhostManagerPresenter _ghostManager;
        private IControls _controls;
        private IConstructionModelView _modelView;
        private bool _dropFinished;

        public ConstructionPresenter(ConstructionsSettingsDefinition constrcutionsSettings, 
            Construction construction, IAssets assets, IConstructionView view,
            GhostManagerPresenter ghostManagerPresenter, IControls controls) : base(view)
        {
            _construction = construction;
            _constructionView = view;
            _constrcutionsSettings = constrcutionsSettings;
            _ghostManager = ghostManagerPresenter ?? throw new ArgumentNullException(nameof(ghostManagerPresenter));
            _controls = controls;

            _constructionView.Position.Value = construction.GetWorldPosition();
            _constructionView.Rotator.Look(ConstructionRotation.ToDirection(construction.Rotation));

            _constructionView.Container.Clear();
            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(assets.GetPrefab(construction.Definition.LevelViewPath));
            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            _controls.ShakeCamera();

            _modelView.Animator.OnFinished += DropFinished;
            _construction.OnDispose += _constructionView.Dispose;
            _ghostManager.OnGhostChanged += UpdateGhost;
            _ghostManager.OnGhostPostionChanged += UpdateGhost;
        }

        protected override void DisposeInner()
        {
            _modelView.Animator.OnFinished -= DropFinished;
            _ghostManager.OnGhostChanged -= UpdateGhost;
            _ghostManager.OnGhostPostionChanged -= UpdateGhost;
            _construction.OnDispose -= _constructionView.Dispose;
        }

        public void UpdateGhost()
        {
            if (!_dropFinished)
                return;

            var ghost = _ghostManager.GetGhost();
            if (ghost != null)
            {
                var distance = ghost.GetTargetPosition().GetDistanceTo(_construction.GetWorldPosition());
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

    }
}
