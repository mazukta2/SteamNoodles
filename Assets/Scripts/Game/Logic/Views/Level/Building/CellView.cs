using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class CellView : View
    {
        public FloatPoint LocalPosition { get; set; }
        public PlacementCellPresenter Presenter { get; private set; }

        public CellView(ILevel level) : base(level)
        {

        }

        public PlacementCellPresenter Init(PlacementFieldPresenter field, IntPoint point, FloatPoint offset)
        {
            var constructionSetttings = Level.Services.Get<DefinitionsService>().Get().Get<ConstructionsSettingsDefinition>();
            if (constructionSetttings == null)
                throw new Exception("Cant finde construction settings definition");

            Presenter = new PlacementCellPresenter(this, field, constructionSetttings, point, offset);
            return Presenter;
        }
    }
}