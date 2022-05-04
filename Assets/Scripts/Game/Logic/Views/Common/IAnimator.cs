using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IAnimator
    {
        public void SwitchTo(string animation);
        public void Play(string animation, bool startAgain = false);
    }
}
