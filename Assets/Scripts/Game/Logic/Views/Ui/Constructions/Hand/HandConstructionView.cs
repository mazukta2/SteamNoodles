using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandConstructionView : View
    {
        private HandConstructionPresenter _presenter;
        public void SetPresenter(HandConstructionPresenter presenter)
        {
            _presenter = presenter;
        }
    }
}
