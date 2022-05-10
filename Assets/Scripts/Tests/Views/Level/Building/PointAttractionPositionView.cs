using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class PointAttractionPositionView : View, IPointAttractionPositionView
    {
        public IPosition PointsAttractionPoint { get; set; } = new PositionMock();
        public IPosition PointsAttractionControlPoint { get; set; } = new PositionMock();

        public PointAttractionPositionView(ILevelView level) : base(level)
        {
        }

    }
}