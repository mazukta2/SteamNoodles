using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private readonly IConstructionView _constructionView;
        private Construction _construction;
        private readonly FieldService _fieldPositionService;
        private readonly ConstructionsService _constructionsService;
        private readonly IAssets _assets;
        private IConstructionModelView _modelView;

        public ConstructionPresenter(IConstructionView view, Construction construction) 
            : this(view, construction,
                  IPresenterServices.Default?.Get<FieldService>(),
                  IPresenterServices.Default?.Get<ConstructionsService>(),
                  IGameAssets.Default,
                  IGameControls.Default)
        {

        }

        public ConstructionPresenter(IConstructionView view,
            Construction construction,
            FieldService fieldPositionService,
            ConstructionsService constructionsService,
            IAssets assets,
            IControls controls) : base(view)
        {
            _constructionView = view ?? throw new ArgumentNullException(nameof(view));
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _fieldPositionService = fieldPositionService ?? throw new ArgumentNullException(nameof(fieldPositionService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _assets = assets ?? throw new ArgumentNullException(nameof(assets));
            _constructionView.Position.Value = _fieldPositionService.GetWorldPosition(construction);
            _constructionView.Rotator.Rotation = FieldRotation.ToDirection(construction.Rotation);

            _modelView = _constructionView.Container.Spawn<IConstructionModelView>(GetPrefab());

            _modelView.Animator.OnFinished += DropFinished;
            _constructionsService.OnRemoved += HandleRemoved;

            _modelView.Animator.Play(IConstructionModelView.Animations.Drop.ToString());
            controls.ShakeCamera();
        }

        protected override void DisposeInner()
        {
            _constructionsService.OnRemoved -= HandleRemoved;

            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.OnFinished -= ExplosionFinished;
        }

        private IViewPrefab GetPrefab()
        {
            return _assets.GetPrefab(_construction.Scheme.LevelViewPath);
        }

        private void DropFinished()
        {
            _modelView.Animator.OnFinished -= DropFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
        }

        private void ExplosionFinished()
        {
            _modelView.Animator.OnFinished -= ExplosionFinished;
            _modelView.Animator.SwitchTo(IConstructionModelView.Animations.Idle.ToString());
            _constructionView.Dispose();
        }

        private void HandleExplosion()
        {
            _modelView.Animator.OnFinished += ExplosionFinished;
            _modelView.Animator.Play(IConstructionModelView.Animations.Explode.ToString());
            _constructionView.EffectsContainer.Spawn(_constructionView.ExplosionPrototype, _fieldPositionService.GetWorldPosition(_construction));
        }

        private void HandleRemoved(Construction obj)
        {
            if (obj.Id == _construction.Id)
                HandleExplosion();
        }

    }
}
