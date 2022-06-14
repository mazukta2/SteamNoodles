using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public interface IScreenManagerView : IView, IViewWithDefaultPresenter
    {
        IViewContainer Screen { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            IPresenterServices.Default.Get<ScreenService>().Bind(this);
        }
    }
}
