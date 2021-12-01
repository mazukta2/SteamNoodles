using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters
{
    public interface IPresenter
    {
        public bool IsDestoyed { get; }
        public void Destroy();
    }
}
