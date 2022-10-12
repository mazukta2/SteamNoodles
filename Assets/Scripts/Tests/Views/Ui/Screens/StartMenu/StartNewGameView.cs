using System;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Levels.StartMenuLevels;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Common;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.StartMenu
{
    public class StartNewGameView : View, IStartNewGameView
    {
        public IButton Button { get; } = new ButtonMock();

        public StartNewGameView(IViews level) : base(level)
        {
            
        }


    }
}

