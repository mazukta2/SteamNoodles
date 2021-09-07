using Assets.Scripts.Models.Levels;
using Assets.Scripts.ViewModels.Buildings;

namespace Assets.Scripts.ViewModels.Level
{
    public class LevelViewModel : GameLevel
    {
        new public PlacementViewModel Placement { get; }
        new public PlayerHandViewModel Hand { get; }

        public LevelViewModel(GameLevel level) : base(level)
        {
            Placement = new PlacementViewModel(base.Placement);
            //Hand = new PlayerHandViewModel(Data);
        }
    }
}
