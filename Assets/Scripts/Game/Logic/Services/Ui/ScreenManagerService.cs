using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services.Ui
{
    public class ScreenManagerService : Disposable, IService
    {
        private ScreenManagerPresenter _presenter;

        public ScreenManagerService(ScreenManagerPresenter presenter)
        {
            _presenter = presenter;
        }

        public ScreenManagerPresenter Get()
        {
            return _presenter;
        }
    }
}
