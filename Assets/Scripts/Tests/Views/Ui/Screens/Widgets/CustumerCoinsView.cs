﻿using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Tests.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets
{
    public class CustumerCoinsView : ViewWithPresenter<CustumerCoinsPresenter>, ICustumerCoinsView
    {
        public IText Text { get; set; } = new TextMock();

        public CustumerCoinsView(IViewsCollection collection) : base(collection)
        {

        }
    }
}