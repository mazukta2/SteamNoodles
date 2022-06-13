using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IGhostManagerView : IViewWithGenericPresenter<GhostManagerPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Container { get;  }
        IViewPrefab GhostPrototype { get; }

        void IViewWithDefaultPresenter.InitDefaultPresenter()
        {
            new GhostManagerPresenter(this);
        }
    }
}