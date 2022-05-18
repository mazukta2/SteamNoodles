using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IDayEndedScreenView : IScreenView
    {
        IButton NextDayButton { get; }
    }
}
