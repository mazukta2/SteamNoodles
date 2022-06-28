using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Services.Flow.Animations
{
    public class DestroyConstructionStep : BaseSequenceStep
    {
        private readonly IDatabase<Construction> _constructions;
        private readonly Construction _construction;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public DestroyConstructionStep(IDatabase<Construction> constructions, Construction construction, IGameTime time, float delay)
        {
            _constructions = constructions;
            _construction = construction;
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
            _constructions.Remove(_construction);

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
