using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Units
{
    public record UnitLookAtEvent : IModelEvent
    {
        public UnitLookAtEvent(GameVector3 target, bool skip)
        {
            Target = target;
            Skip = skip;
        }

        public GameVector3 Target { get; }
        public bool Skip { get; }
    }
}
