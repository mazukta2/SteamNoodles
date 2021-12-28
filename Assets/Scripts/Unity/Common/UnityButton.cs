using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnity.Assets.Scripts.Unity.Common
{
    public class UnityButton : IButtonView
    {

        public event Action OnDispose = delegate { };

        public bool IsShowing => throw new NotImplementedException();
        public bool IsDisposed => throw new NotImplementedException();

        public void Click()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetAction(Action action)
        {
            throw new NotImplementedException();
        }

        public void SetShowing(bool value)
        {
            throw new NotImplementedException();
        }
    }
}
