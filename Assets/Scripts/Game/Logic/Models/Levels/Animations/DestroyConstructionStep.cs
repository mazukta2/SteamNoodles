using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class DestroyConstructionStep : BaseSequenceStep
    {
        private readonly ConstructionDestroyedOnWaveEndEvent _constructionEvent;
        private readonly IGameTime _time;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public DestroyConstructionStep(ConstructionDestroyedOnWaveEndEvent constructionEvent, IGameTime time, float delay)
        {
            _constructionEvent = constructionEvent;
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
            _constructionEvent.Fire();

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
