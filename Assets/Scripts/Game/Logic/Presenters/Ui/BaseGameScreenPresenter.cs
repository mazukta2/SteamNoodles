using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Unity.Views;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui
{
    public abstract class BaseGameScreenPresenter : BasePresenter
    {
        public BaseGameScreenPresenter(ViewPresenter view) : base(view) { }
    }
}
