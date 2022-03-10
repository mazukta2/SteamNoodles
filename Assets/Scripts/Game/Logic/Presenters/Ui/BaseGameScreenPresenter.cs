using Game.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public abstract class BaseGameScreenPresenter : BasePresenter
    {
        public ScreenManagerPresenter Manager { get; private set; }
        public BaseGameScreenPresenter(ScreenManagerPresenter manager, View view) : base(view) 
        {
            Manager = manager;
        }
    }
}
