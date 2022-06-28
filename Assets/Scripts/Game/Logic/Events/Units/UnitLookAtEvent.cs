using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Databases;

namespace Game.Assets.Scripts.Game.Logic.Events.Units
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
