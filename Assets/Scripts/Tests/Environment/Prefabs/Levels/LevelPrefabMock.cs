using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Mocks.Levels
{
    public abstract class LevelPrefabMock
    {
        public abstract void FillLevel(LevelView level);
    }
}
