using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        public Modes Mode { get => _mode; set => SetMode(value); }
        private readonly IPresenterRepository<ConstructionCard> _model;
        private readonly IPresenterRepository<Construction> _constructions;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IHandView _view;
        private Modes _mode;

        public HandPresenter(IPresenterRepository<ConstructionCard> model, IPresenterRepository<Construction> constructions, ScreenManagerPresenter screenManager, IHandView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            var cards = model.Get();
            foreach (var item in cards)
                HandleCardAdded(item, null);
            _model.OnAdded += HandleCardAdded;

            _view.CancelButton.SetAction(CancelClick);
            _view.Animator.SwitchTo(Modes.Disabled.ToString());
        }

        protected override void DisposeInner()
        {
            _model.OnAdded -= HandleCardAdded;
        }

        private void HandleCardAdded(EntityLink<ConstructionCard> entity, ConstructionCard obj)
        {
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            new HandConstructionPresenter(entity, _screenManager, _constructions, view);
        }

        private void CancelClick()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        private void SetMode(Modes value)
        {
            _mode = value;
            _view.Animator.Play(value.ToString());
        }

        public enum Modes
        {
            Disabled,
            Choose,
            Build
        }
    }
}
