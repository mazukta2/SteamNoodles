using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Keepers
{
    public interface IMaker<T>
    {
        T Create();
    }
}
