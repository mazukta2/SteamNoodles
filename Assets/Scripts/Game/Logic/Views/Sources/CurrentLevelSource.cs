using Game.Assets.Scripts.Game.Logic.Models.Levels;

namespace Game.Assets.Scripts.Game.Logic.Views.Sources
{
    public class CurrentLevelSource : LevelSource
    {
        private GameLevel _level;

        public override GameLevel GetLevel()
        {
            return _level;
        }

        public void SetLevel(GameLevel level)
        {
            _level = level;
            FireOnChanged(level);
        }
    }
}
