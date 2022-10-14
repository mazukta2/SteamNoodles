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
    public class DialogView : View, IDialogView
    {
        public AnimatorMock Animator { get; set; } = new AnimatorMock();
        IAnimator IDialogView.Animator => Animator;

        public ButtonMock Button { get; set; } = new ButtonMock();
        IButton IDialogView.Next => Button;

        public DialogView(IViews collection) : base(collection)
        {

        }
    }
}
