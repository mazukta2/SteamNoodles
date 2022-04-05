using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IPresenterView<T> : IView where T :BasePresenter
    {
        T Presenter { get; set; }
    }
}
