using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations
{
    public class EmptyLevelVariation : LevelVariation
    {
        public override ILevel CreateModel(LevelDefinition definition)
        {
            return null;
        }

        public override void Validate()
        {
        }
    }
}
