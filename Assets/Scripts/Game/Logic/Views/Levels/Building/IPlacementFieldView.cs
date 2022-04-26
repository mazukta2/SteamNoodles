using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public interface IPlacementFieldView : IPresenterView, IViewWithAutoInit
    {
        IPlacementManagerView Manager { get; }

        void IViewWithAutoInit.Init()
        {
            new PlacementFieldPresenter(GhostManagerService.Default.Get(), Level.Model.Constructions.Placement, this,
                IDefinitions.Default.Get<ConstructionsSettingsDefinition>(),
                Manager.Presenter, IAssets.Default);
        }
    }
}