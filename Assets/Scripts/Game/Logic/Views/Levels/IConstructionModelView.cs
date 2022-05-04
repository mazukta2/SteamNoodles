using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IConstructionModelView : IView
    {
        IAnimator Animator { get; }
        IAnimator BorderAnimator { get; }
        IFloat Shrink { get; }

        public enum Animations
        {
            Idle,
            Dragging,
        }

        public enum BorderAnimations
        {
            Idle,
            Disallowed
        }
    }
}
