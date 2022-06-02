using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Game.Logic.Views.Ui;

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