using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Environment.Common
{
    public class Switcher<T> : ISwitcher<T> where T : Enum
    {
        public T Value { get; set; }
    }
}
