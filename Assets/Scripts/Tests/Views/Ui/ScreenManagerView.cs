using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Tests.Views.Common.Creation;
using System;

namespace Game.Assets.Scripts.Tests.Views.Ui
{
    public class ScreenManagerView : View, IScreenManagerView
    {
        public IViewContainer Screen { get; }

        public ScreenManagerView(IViewsCollection level) : base(level)
        {
            Screen = new ContainerViewMock(level);
        }
    }
}
