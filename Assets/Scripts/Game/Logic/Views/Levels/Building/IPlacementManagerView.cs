using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IPlacementManagerView : IPresenterView, IViewWithAutoInit
    {
        IViewContainer ConstrcutionContainer { get; }
        IViewPrefab ConstrcutionPrototype { get; }
        IViewContainer CellsContainer { get;  }
        IViewPrefab Cell { get; }
        PlacementManagerPresenter Presenter { get; }

        void IViewWithAutoInit.Init()
        {
            var service = Level.Services.Get<ScreenManagerService>();
            new PlacementManagerPresenter(Level.Model.Constructions, service.Get(), this);
        }
    }
}