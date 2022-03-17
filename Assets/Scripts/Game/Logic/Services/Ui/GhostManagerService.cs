using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services.Ui
{
    public class GhostManagerService : Disposable, IService
    {
        private GhostManagerPresenter _presenter;

        public GhostManagerService(GhostManagerPresenter presenter)
        {
            _presenter = presenter;
        }

        public GhostManagerPresenter Get()
        {
            return _presenter;
        }
    }
}
