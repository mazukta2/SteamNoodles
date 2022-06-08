using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public interface IHandView : IViewWithGenericPresenter<HandPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Cards { get;  }
        IViewPrefab CardPrototype { get; }
        IButton CancelButton { get; }
        IAnimator Animator { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new HandPresenter(this);
        }
    }
}
