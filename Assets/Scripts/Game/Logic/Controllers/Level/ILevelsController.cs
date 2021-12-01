using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Controllers.Level
{
    public interface ILevelsController
    {
        void Load(ILevelSettings prototype, Action onFinished);
    }
}
