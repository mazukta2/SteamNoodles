using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using Tests.Tests.Mocks.Views.Common;

namespace Game.Tests.Mocks.Views.Clashes
{
    public class ClashesView : TestView, IClashesView 
    {
        private Action _startClashAction;
        public bool ButtonShowed { get; private set; }

        public void SetStartClashAction(Action onStartClash)
        {
            _startClashAction = onStartClash;
        }

        public void ShowButton(bool v)
        {
            ButtonShowed = v;
        }
        protected override void DisposeInner()
        {
        }
    }
}
