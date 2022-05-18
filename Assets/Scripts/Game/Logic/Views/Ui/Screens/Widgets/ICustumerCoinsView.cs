using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface ICustumerCoinsView : IViewWithGenericPresenter<CustumerCoinsPresenter>, IViewWithDefaultPresenter
    {
        IText Text { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new CustumerCoinsPresenter(ICurrentLevel.Default.Resources.Coins, this);
        }
    }
}
