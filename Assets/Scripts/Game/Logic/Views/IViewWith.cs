using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IViewWith<T> : IView where T : IPresenter
    {
        T Presenter { get; }
    }
}
