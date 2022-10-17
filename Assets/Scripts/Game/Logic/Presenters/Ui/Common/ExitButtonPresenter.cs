﻿using System;
using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common
{
    public class ExitButtonPresenter : Disposable, IPresenter
    {
        private IExitGameButtonView _view;
        private GameApplication _core;

        public ExitButtonPresenter(IExitGameButtonView view) : this(view, IInfrastructure.Default.Application)
        {

        }

        public ExitButtonPresenter(IExitGameButtonView view, GameApplication core)
        {
            _view = view;
            _core = core;
            _view.Button.SetAction(ExitClick);
        }

        private void ExitClick()
        {
            _core.Dispose();
        }
    }
}

