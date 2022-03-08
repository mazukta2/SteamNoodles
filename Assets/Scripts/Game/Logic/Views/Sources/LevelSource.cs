using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Sources
{
    public abstract class LevelSource : View
    {
        public event Action<GameLevel> OnChanged = delegate { };
        public abstract GameLevel GetLevel();
        protected void FireOnChanged(GameLevel level)
        {
            OnChanged(level);
        }
    }
}
