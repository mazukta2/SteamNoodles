using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter
    {
        private readonly PlayerHand _model;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly HandViewPresenter _view;

        public HandPresenter(PlayerHand model, ScreenManagerPresenter screenManager, HandViewPresenter view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            foreach (var item in model.Cards)
                ScnemeAddedHandle(item);
            _model.OnAdded += ScnemeAddedHandle;
        }

        protected override void DisposeInner()
        {
            _model.OnAdded -= ScnemeAddedHandle;
        }

        private void ScnemeAddedHandle(ConstructionCard obj)
        {
            var viewPresenter = _view.CardPrototype.Create<HandConstructionViewPresenter>(_view.Cards);
            viewPresenter.Init(_screenManager, obj);
        }
    }
}
