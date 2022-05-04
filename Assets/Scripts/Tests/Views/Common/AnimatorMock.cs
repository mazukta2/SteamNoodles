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
        public void Play(string animation, bool startAgain = false)
        {
            Animation = animation;
        }

        public void SwitchTo(string animation)
        {
            Animation = animation;
        }
    }
}
