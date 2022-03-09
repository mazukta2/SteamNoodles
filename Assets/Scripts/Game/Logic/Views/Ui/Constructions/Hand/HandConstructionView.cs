using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : View
    {
        private HandConstructionViewPresenter _viewPresenter;

        protected override void CreatedInner()
        {
            _viewPresenter = new HandConstructionViewPresenter(Level);
        }

        protected override void DisposeInner()
        {
            _viewPresenter.Dispose();
        }
    }

    public class HandConstructionViewPresenter : ViewPresenter
    {
        private HandConstructionPresenter _presenter;
        public HandConstructionViewPresenter(ILevel level) : base(level)
        {
        }

        public void Init(ConstructionCard construction)
        {
            _presenter = new HandConstructionPresenter(this);
        }
    }
}
