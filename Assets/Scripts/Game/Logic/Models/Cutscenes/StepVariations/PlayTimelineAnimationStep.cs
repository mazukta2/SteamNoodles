using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class PlayTimelineAnimationStep : CutsceneStep
    {
        private IControls _controls;
        private string _animationName;

        public PlayTimelineAnimationStep(string name) : this(name, IGameControls.Default)
        {

        }

        public PlayTimelineAnimationStep(string animationName, IControls controls)
        {
            _controls = controls;
            _animationName = animationName;
        }

        protected override void DisposeInner()
        {
            _controls.OnTimelineAnimationFinished -= ControlsOnOnTimelineAnimationFinished;
        }

        public override void Play()
        {
            _controls.OnTimelineAnimationFinished += ControlsOnOnTimelineAnimationFinished;
            _controls.PlayTimelineAnimation(_animationName);
        }

        private void ControlsOnOnTimelineAnimationFinished(string name)
        {
            if (name != _animationName)
                return;
            
            FireOnFinished();
        }

    }
}

