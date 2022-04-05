using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IPresenterIniter<T>
    {
        void SetPresenter(T presenter);
    }
}
