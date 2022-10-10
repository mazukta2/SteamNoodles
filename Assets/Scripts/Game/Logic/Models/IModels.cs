using System;
using Game.Assets.Scripts.Game.Logic.Models.Levels;

namespace Game.Assets.Scripts.Game.Logic.Models
{
    public interface IModels
    {
        public static IModels Default { get; set; }

        void Add<T>(T model);
        T Find<T>();
    }
}

