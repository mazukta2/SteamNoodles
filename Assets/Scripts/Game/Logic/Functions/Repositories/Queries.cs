using Game.Assets.Scripts.Game.Logic.Models.Entities;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Functions.Repositories
{
    public static class Queries
    {
        
        public static ISingleQuery<T> AsQuery<T>(this T d) where T : Entity
        {
            return new StaticEntityQuery<T>(d);
        }
        
    }
}