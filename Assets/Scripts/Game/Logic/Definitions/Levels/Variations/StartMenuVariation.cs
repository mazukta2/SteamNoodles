using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations
{
    public class StartMenuVariation : LevelVariation
    {
        public override ILevel CreateModel(LevelDefinition definition, IModels models)
        {
            return new StartMenu(this, IGameRandom.Default, IGameTime.Default, IGameDefinitions.Default);
        }

        public override void Validate()
        {
        }
    }
}
