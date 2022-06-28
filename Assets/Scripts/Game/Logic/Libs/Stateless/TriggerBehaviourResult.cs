﻿using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Libs.Stateless
{
    public partial class StateMachine<TState, TTrigger>
    {
        internal class TriggerBehaviourResult
        {
            public TriggerBehaviourResult(TriggerBehaviour handler, ICollection<string> unmetGuardConditions)
            {
                Handler = handler;
                UnmetGuardConditions = unmetGuardConditions;
            }
            public TriggerBehaviour Handler { get; }
            public ICollection<string> UnmetGuardConditions { get; }
        }
    }
}
