﻿using Game.Assets.Scripts.Game.Environment.Engine.Controls;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandPresenter : BasePresenter<IHandView>
    {
        private readonly PlayerHand _model;
        private readonly ScreenManagerPresenter _screenManager;
        private readonly IHandView _view;

        public HandPresenter(PlayerHand model, ScreenManagerPresenter screenManager, IHandView view) : base(view)
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
            var view = _view.Cards.Spawn<IHandConstructionView>(_view.CardPrototype);
            new HandConstructionPresenter(_screenManager, view, obj, _view.Level.Pointer);
        }
    }
}
