using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        public ConstructionCard CurrentCard { get; }

        private IBuildScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Resources _resources;
        private ConstructionsSettingsDefinition _constrcutionsSettings;
        private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public BuildScreenPresenter(IBuildScreenView view, ScreenManagerPresenter screenManager, 
            Resources resources, ConstructionCard constructionCard, ConstructionsSettingsDefinition constrcutionsSettings) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _constrcutionsSettings = constrcutionsSettings ?? throw new ArgumentNullException(nameof(constrcutionsSettings));
            _view.CancelButton.SetAction(CancelClick);

            CurrentCard = constructionCard;
        }

        protected override void DisposeInner()
        {
        }

        private void CancelClick()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        public void UpdatePoints(FloatPoint position, int points, IReadOnlyDictionary<Construction, int> bonuses)
        {
            _view.Points.Value = $"{points.GetSignedNumber()}";
            _view.Points.Position = position;
            _view.CurrentPoints.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";

            var newPoints =_resources.Points.GetChangedValue(points);
            if (newPoints.Value < _resources.Points.Value)
            {
                _view.PointsProgress.MainValue = newPoints.Progress;
                if (newPoints.CurrentLevel == _resources.Points.CurrentLevel)
                    _view.PointsProgress.RemovedValue = _resources.Points.Progress;
                else
                    _view.PointsProgress.RemovedValue = 1;
                _view.PointsProgress.AddedValue = 0;
            }
            else
            {
                _view.PointsProgress.MainValue = _resources.Points.Progress;
                if (newPoints.CurrentLevel == _resources.Points.CurrentLevel)
                    _view.PointsProgress.AddedValue = newPoints.Progress;
                else
                    _view.PointsProgress.AddedValue = 1;
                _view.PointsProgress.RemovedValue = 0;
            }

            UpdateBonuses(bonuses);
        }

        public class BuildScreenCollection : ScreenCollection
        {
            public void Open(ConstructionCard constructionCard)
            {
                Manager.Open<IBuildScreenView>(Init);

                object Init(IBuildScreenView screenView, ScreenManagerPresenter managerPresenter)
                {
                    return new BuildScreenPresenter(screenView, managerPresenter, 
                        screenView.Level.Model.Resources, constructionCard, 
                        IDefinitions.Default.Get<ConstructionsSettingsDefinition>());
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
                text.Position = position.GetViewPositionByWorldPosition(item.Key.CellPosition);
            }
        }

    }
}
