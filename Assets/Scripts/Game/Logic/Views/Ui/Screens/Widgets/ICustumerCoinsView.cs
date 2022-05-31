using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface ICustumerCoinsView : IViewWithGenericPresenter<CustumerCoinsPresenter>, IViewWithDefaultPresenter
    {
        IText Text { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new CustumerCoinsPresenter(IStageLevel.Default.Coins, this);
        }
    }
}
