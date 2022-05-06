using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IAnimator
    {
        event Action OnFinished;
        void SwitchTo(string animation);
        void Play(string animation, bool startAgain = false);
    }
}
