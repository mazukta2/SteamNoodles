using Game.Assets.Scripts.Game.Logic.Common.Animations;
using Game.Assets.Scripts.Game.Logic.Common.Calculations;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Flow.Animations
{
    public class DestroyConstructionStep : BaseSequenceStep
    {
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly ConstructionEntity _constructionEntity;
        private readonly float _delay;
        private readonly TimeUpdater _updater;

        public DestroyConstructionStep(IDatabase<ConstructionEntity> constructions, ConstructionEntity constructionEntity, IGameTime time, float delay)
        {
            _constructions = constructions;
            _constructionEntity = constructionEntity;
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
            _constructions.Remove(_constructionEntity.Id);

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
