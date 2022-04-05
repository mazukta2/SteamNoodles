using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IEnabler<T> where T : Enum
    {
        T Value { get; set; }
    }
}
