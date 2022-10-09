using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Animations
{
    public class DestroyConstructionStep : BaseSequenceStep
    {
        private readonly Construction _construction;
        private readonly IGameTime _time;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public DestroyConstructionStep(Construction construction, IGameTime time, float delay)
        {
            _construction = construction;
            _time = time;
            _delay = delay;
            _updater = new TimeUpdater(time, delay);
        }

        protected override void DisposeInner()
        {
            _updater.Dispose();
            _updater.OnUpdate -= FireOnFinished;
        }

        public override void Play()
        {
            _construction.Explode();

            if (_delay == 0)
            {
                FireOnFinished();
                return;
            }

            _updater.OnUpdate += FireOnFinished;
            _updater.Start();
        }
    }
}
