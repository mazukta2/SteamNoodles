using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public interface IMainScreenView : IScreenView
    {
        void Init()
        {
            new MainScreenPresenter(this);
        }
    }
}
