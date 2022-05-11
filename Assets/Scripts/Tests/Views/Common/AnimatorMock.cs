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
            var oldAnimation = Animation;
            Animation = animation;

            if (startAgain || oldAnimation != animation)
                OnFinished();
        }

        public void SwitchTo(string animation)
        {
            var oldAnimation = Animation;
            Animation = animation;
            if (oldAnimation != animation)
                OnFinished();
        }
    }
}
