using Game.Assets.Scripts.Game.Environment.Engine;
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
        int Id { get; }

        void IViewWithAutoInit.Init()
        {
            var service = Level.Services.Get<GhostManagerService>();
            var field = Level.Model.Constructions.Placements.ElementAt(Id);

            new PlacementFieldPresenter(service.Get(), field, this,
                Level.Engine.Definitions.Get<ConstructionsSettingsDefinition>(),
                Manager.Presenter, Level.Engine.Assets);
        }
    }
}