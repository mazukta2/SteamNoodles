using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter
    {
        private Construction _construction;
        private ConstructionView _constructionView;
        private ConstructionsSettingsDefinition _constrcutionsSettings;

        public ConstructionPresenter(ConstructionsSettingsDefinition constrcutionsSettings, Construction construction, IAssets assets, ConstructionView view) : base(view)
        {
            _construction = construction;
            _constructionView = view;
            _constrcutionsSettings = constrcutionsSettings;

            var position = new FieldPositionsCalculator(constrcutionsSettings.CellSize, construction.Definition.GetRect(construction.Rotation));
            _constructionView.LocalPosition.Value = position.GetViewPositionByWorldPosition(construction.Position);

            _constructionView.Container.Clear();
            _constructionView.Container.Spawn<ConstructionModelView>(assets.GetConstruction(construction.Definition.LevelViewPath));
        }
    }
}
