using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IView
    {
        event Action OnDispose;
        bool IsDisposed { get; }
        void Dispose();
        LevelView Level { get; }
        bool IsHighlihgted { get; }
        event Action OnHighlihgtedEnter;
        event Action OnHighlihgtedExit;
    }
}
