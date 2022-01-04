using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Controllers.Level
{
    public interface IAssetsController
    {
        ISprite GetSprite(string path);
        IVisual GetVisual(string path);
    }
}
