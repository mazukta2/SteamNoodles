using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Assets;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public interface IScreenManagerView : IViewWithGenericPresenter<ScreenManagerPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Screen { get; }
        static IScreenManagerView Default { get; set; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            Default = this;
            new ScreenManagerPresenter(this, IGameAssets.Default);
        }
    }
}
