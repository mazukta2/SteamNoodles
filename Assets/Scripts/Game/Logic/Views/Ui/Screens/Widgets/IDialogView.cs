using System;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface IDialogView : IView
    {
        IAnimator Animator { get; }
        IButton Next { get; }
    }
}

