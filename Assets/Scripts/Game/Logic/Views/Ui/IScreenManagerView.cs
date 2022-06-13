using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public interface IScreenManagerView : IViewWithGenericPresenter<ScreenManagerPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Screen { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            ScreenManagerPresenter.Default = new ScreenManagerPresenter(this);
        }
    }
}
