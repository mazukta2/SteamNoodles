using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Tests.Mocks.Views.Common
{
    public class TestView : IView
    {
        public bool IsDestoyed { get; private set; }

        public void Destroy()
        {
            IsDestoyed = true;
        }
    }
}
