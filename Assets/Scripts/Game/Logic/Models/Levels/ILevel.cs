using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public interface ILevel : IDisposable
    {
        public static ILevel Default { get; set; }
        
        bool IsDisposed { get; }

        LevelDefinition Definition { get; }
    }
}
