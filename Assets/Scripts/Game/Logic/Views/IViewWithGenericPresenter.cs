using Game.Assets.Scripts.Game.Logic.Presenters;

namespace Game.Assets.Scripts.Game.Logic.Views
{
    public interface IViewWithGenericPresenter<T> : IViewWithPresenter where T : IPresenter
    {
    }
}
