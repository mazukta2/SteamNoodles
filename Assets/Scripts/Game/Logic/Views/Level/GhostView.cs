using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class GhostView : View
    {
        private GhostPresenter _presenter;

        public GhostView(ILevel level) : base(level)
        {
            _presenter = new GhostPresenter(this);
        }
    }
}
