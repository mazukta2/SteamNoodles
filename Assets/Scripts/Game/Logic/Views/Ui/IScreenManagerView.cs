using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public interface IScreenManagerView : IViewWithGenericPresenter<ScreenManagerPresenter>, IViewWithAutoInit
    {
        IViewContainer Screen { get; }

        void IViewWithAutoInit.Init()
        {
            new ScreenManagerPresenter(this, IAssets.Default);
        }
    }
}
