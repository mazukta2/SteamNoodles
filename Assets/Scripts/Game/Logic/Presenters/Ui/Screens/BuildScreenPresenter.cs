using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        public ConstructionCard CurrentCard { get; }

        private IBuildScreenView _view;
        private ConstructionsSettingsDefinition _constrcutionsSettings;
        private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public BuildScreenPresenter(IBuildScreenView view,
            ConstructionCard constructionCard, ConstructionsSettingsDefinition constrcutionsSettings, HandPresenter handPresenter) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constrcutionsSettings = constrcutionsSettings ?? throw new ArgumentNullException(nameof(constrcutionsSettings));

            handPresenter.Mode = HandPresenter.Modes.Build;
            
            CurrentCard = constructionCard;
        }

        protected override void DisposeInner()
        {
        }

        public void UpdatePoints(FloatPoint position, int points, IReadOnlyDictionary<Construction, int> bonuses)
        {
            _view.Points.Value = $"{points.GetSignedNumber()}";
            _view.Points.Position = position;

            UpdateBonuses(bonuses);
        }

        public class BuildScreenCollection : ScreenCollection
        {
            public void Open(ConstructionCard constructionCard)
            {
                Manager.Open<IBuildScreenView>(Init);

                object Init(IBuildScreenView screenView, ScreenManagerPresenter managerPresenter)
                {
                    return new BuildScreenPresenter(screenView, constructionCard, 
                        IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), IHandView.Default.Presenter);
                }
            }
        }

        private void UpdateBonuses(IReadOnlyDictionary<Construction, int> newBonuses)
        {
            foreach (var item in _bonuses.ToList())
            {
                if (!newBonuses.ContainsKey(item.Key))
                {
                    _bonuses[item.Key].Dispose();
                    _bonuses.Remove(item.Key);
                }    
            }

            foreach (var item in newBonuses)
            {
                if (!_bonuses.ContainsKey(item.Key))
                {
                    var view = _view.AdjacencyContainer.Spawn<IAdjacencyTextView>(_view.AdjacencyPrefab);
                    _bonuses[item.Key] = view;
                }
            }

            foreach (var item in newBonuses)
            {
                var text =_bonuses[item.Key].Text;
                text.Value = $"{item.Value}";

                var position = new FieldPositionsCalculator(_constrcutionsSettings.CellSize, item.Key.Definition.GetRect(item.Key.Rotation));
                text.Position = position.GetPositionByWorldPosition(item.Key.CellPosition);
            }
        }

    }
}
