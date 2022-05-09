using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IGhostManagerView : IViewWithGenericPresenter<GhostManagerPresenter>, IViewWithDefaultPresenter
    {
        IViewContainer Container { get;  }
        IViewPrefab GhostPrototype { get; }

        void IViewWithDefaultPresenter.Init()
        {
            new GhostManagerPresenter(IScreenManagerPresenter.Default, IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IControls.Default,
                Level.Model.Constructions, this);
        }
    }
}