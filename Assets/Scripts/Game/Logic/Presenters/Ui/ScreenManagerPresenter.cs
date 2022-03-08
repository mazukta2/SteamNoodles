﻿using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public class ScreenManagerPresenter : BasePresenter
    {
        private readonly ScreenManagerView _view;
        private readonly IScreenAssets _screenAssets;

        public ScreenManagerPresenter(ScreenManagerView view, IScreenAssets screenAssets) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenAssets = screenAssets ?? throw new ArgumentNullException(nameof(screenAssets));

            GetScreen<MainScreenView>().Open();
        }

        protected override void DisposeInner()
        {

        }

        public ScreenController GetScreen<T>() where T : ScreenView
        {
            return new ParametlessScreenController<T>(this);
        }

        public class ParametlessScreenController<TScreen> : ScreenController where TScreen : ScreenView
        {
            private readonly ScreenManagerPresenter _manager;

            public ParametlessScreenController(ScreenManagerPresenter manager)
            {
                _manager = manager ?? throw new ArgumentNullException(nameof(manager));
            }

            public override void Open()
            {
                var screenAsset = _manager._screenAssets.GetScreen<TScreen>();
                if (screenAsset == null)
                    throw new Exception($"Cant find {typeof(TScreen).Name} view");

                _manager._view.Clear();
                var view = _manager._view.Create(screenAsset);
                view.SetManager(_manager);
            }
        }
    }
}