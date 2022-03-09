using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Constructions
{
    public class ConstructionCard : Disposable
    {

        //public Point CellSize => _prototype.Size;
        //public Requirements Requirements => _prototype.Requirements;
        //public ISprite HandIcon => _prototype.HandIcon;
        //public IVisual BuildingView => _prototype.BuildingView;
        //public IConstructionSettings Settings => _prototype;

        private ConstructionDefinition _definition;
        public ConstructionCard(ConstructionDefinition definition)
        {
            _definition = definition;
        }

        //public Point[] GetOccupiedSpace(Point position)
        //{
        //    return new ConstructionSettingsFunctions(_prototype).GetOccupiedSpace(position);
        //}
    }
}
