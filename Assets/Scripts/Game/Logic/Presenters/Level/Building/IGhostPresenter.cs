using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public interface IGhostPresenter
    {
        event Action OnGhostChanged;
        event Action OnGhostPostionChanged;

        int GetPointChanges();
    }
}
