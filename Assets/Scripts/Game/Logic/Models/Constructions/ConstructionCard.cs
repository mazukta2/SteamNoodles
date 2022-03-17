using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionCard : Disposable
    {
        public ConstructionDefinition Definition { get; }

        public ConstructionCard(ConstructionDefinition definition)
        {
            Definition = definition;
        }
    }
}
