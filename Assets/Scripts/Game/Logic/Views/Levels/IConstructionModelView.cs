using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
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
            Drop,
            Explode,
        }

        public enum BorderAnimations
        {
            Idle,
            Disallowed
        }
    }
}
