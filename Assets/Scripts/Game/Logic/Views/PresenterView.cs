using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public abstract class PresenterView<T> : View, IPresenterView<T> where T : BasePresenter
    {
        public T Presenter { get; set; }
        protected PresenterView(ILevel level) : base(level)
        {
        }
    }
}
