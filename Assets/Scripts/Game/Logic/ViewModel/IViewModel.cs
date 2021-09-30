using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.ViewModel
{
    public interface IViewModel
    {
        public bool IsDestoyed { get; }
        public void Destroy();
    }
}
