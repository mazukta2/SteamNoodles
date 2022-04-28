using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Level;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements
{
    public class ConstructionPresenter : BasePresenter<IConstructionView>
    {
        private Construction _construction;
        private IConstructionView _constructionView;
        private ConstructionsSettingsDefinition _constrcutionsSettings;

        public ConstructionPresenter(ConstructionsSettingsDefinition constrcutionsSettings, Construction construction, IAssets assets, IConstructionView view) : base(view)
        {
            _construction = construction;
            _constructionView = view;
            _constrcutionsSettings = constrcutionsSettings;

            var position = new FieldPositionsCalculator(constrcutionsSettings.CellSize, construction.Definition.GetRect(construction.Rotation));
            _constructionView.Position.Value = position.GetViewPositionByWorldPosition(construction.CellPosition);
            _constructionView.Rotator.Look(ConstructionRotation.ToDirection(construction.Rotation));

            _constructionView.Container.Clear();
            _constructionView.Container.Spawn<IConstructionModelView>(assets.GetPrefab(construction.Definition.LevelViewPath));
        }
    }
}
