using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

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
