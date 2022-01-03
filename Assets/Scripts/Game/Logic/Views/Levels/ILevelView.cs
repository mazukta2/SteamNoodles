using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Units;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
{
    public interface ILevelView : IView
    {
        public IScreenView Screen { get; }
        public IUnitsView Units { get; }
        public IPlacementView Placement { get; }
    }
}
