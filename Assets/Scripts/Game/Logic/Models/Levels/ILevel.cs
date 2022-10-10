using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public interface ILevel : IDisposable
    {
        bool IsDisposed { get; }
        public string SceneName { get; }
        public void Start();
    }
}
