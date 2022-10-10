using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Tests.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets
{
    public class WaveWidgetViewMock : ViewWithPresenter<WaveWidgetPresenter>, IWaveWidgetView
    {
        public IButton NextWaveButton { get; set; } = new ButtonMock();
        public IButton FailWaveButton { get; set; } = new ButtonMock();
        public IProgressBar NextWaveProgress { get; set; } = new ProgressBar();
        public AnimatorMock WaveButtonAnimator { get; set; } = new AnimatorMock();
        IAnimator IWaveWidgetView.WaveButtonAnimator => WaveButtonAnimator;

        public WaveWidgetViewMock(IViews collection) : base(collection)
        {

        }
    }
}
