﻿using Game.Assets.Scripts.Game.Logic.Common.Animations;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class ActionStep : BaseSequenceStep
    {
        private Action _action;

        public ActionStep(Action action)
        {
            _action = action;
        }

        protected override void DisposeInner()
        {
        }

        public override void Play()
        {
            _action();
            FireOnFinished();
        }
    }
}