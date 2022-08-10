﻿using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public interface IScreenOpener
    {
        void Open<TScreen>(Func<TScreen, ScreenManagerPresenter, object> init) where TScreen : class, IScreenView;
    }
}