using Game.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public abstract class BaseGameScreenPresenter<TView, TPresenter> : BasePresenter<TView, TPresenter>
        where TView : IPresenterView<TPresenter>, IPresenterIniter<TPresenter>
        where TPresenter : BasePresenter<TView, TPresenter>
    {
        public ScreenManagerPresenter Manager { get; private set; }
        public BaseGameScreenPresenter(ScreenManagerPresenter manager, TView view) : base(view) 
        {
            Manager = manager;
        }
    }
}
