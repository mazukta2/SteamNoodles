using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public record UnitLookAtEvent(GameVector3 target, bool skip) : IModelEvent;
}
