using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Controllers
{
    public interface IGameController
    {
        ILevelsController Levels { get; }
        ISettingsController Settings { get; }
        IAssetsController Assets { get; }
        void SetTimeMover(Action<float> moveTime);
    }
}
