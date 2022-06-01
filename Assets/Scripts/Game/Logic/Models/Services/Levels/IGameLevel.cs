using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Levels
{
    public interface IGameLevel : IDisposable
    {
        public static IGameLevel Default { get; set; }

        bool IsDisposed { get; }

        LevelDefinition Definition { get; }
    }
}
