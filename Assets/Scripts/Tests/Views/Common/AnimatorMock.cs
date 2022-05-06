using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Common
{
    public class AnimatorMock : IAnimator
    {
        public string Animation { get; private set; }

        public event Action OnFinished = delegate { };

        public void Play(string animation, bool startAgain = false)
        {
            Animation = animation;
            OnFinished();
        }

        public void SwitchTo(string animation)
        {
            Animation = animation;
            OnFinished();
        }
    }
}
