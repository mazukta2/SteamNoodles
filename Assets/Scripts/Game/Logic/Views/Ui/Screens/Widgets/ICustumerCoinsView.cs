using Game.Assets.Scripts.Game.Logic.Infrastructure.Flow;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets
{
    public interface ICustumerCoinsView : IViewWithGenericPresenter<CustumerCoinsPresenter>, IViewWithDefaultPresenter
    {
        IText Text { get; }
        IAnimator Animator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new CustumerCoinsPresenter(IModels.Default.Find<Coins>(), this);
        }
    }
}
