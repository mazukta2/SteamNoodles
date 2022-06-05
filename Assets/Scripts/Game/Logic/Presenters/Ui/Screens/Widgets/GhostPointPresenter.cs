using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class GhostPointPresenter : BasePresenter<IGhostPointsView>
    {
        private IGhostPointsView _view;
        private readonly BuildingModeService _buildingModeService;
        private readonly ConstructionsService _constructionsService;

        //private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public GhostPointPresenter(IGhostPointsView view) : this(
                view,
                IPresenterServices.Default?.Get<BuildingModeService>(),
                IPresenterServices.Default?.Get<ConstructionsService>())
        {
        }

        public GhostPointPresenter(IGhostPointsView view,
            BuildingModeService buildingModeService,
            ConstructionsService constructionsService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _buildingModeService = buildingModeService ?? throw new ArgumentNullException(nameof(buildingModeService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));

            _buildingModeService.OnPositionChanged += UpdatePoints;
            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            _buildingModeService.OnPositionChanged -= UpdatePoints;
        }

        public void UpdatePoints()
        {
            //var points = _constructionsService.GetPoints(_entity,
            //    _buildingModeService.GetPosition(), _buildingModeService.GetRotation()).Value;

            //var worldPosition = _fieldService.GetWorldPosition(_buildingModeService.GetPosition(), size);

            //_view.Points.Value = $"{points.GetSignedNumber()}";
            //_view.Points.Position = worldPosition;

            //_buildingModeService.Get

            //UpdateBonuses(bonuses);
        }

        private void UpdateBonuses(IReadOnlyDictionary<Construction, BuildingPoints> newBonuses)
        {
            //foreach (var item in _bonuses.ToList())
            //{
            //    if (!newBonuses.ContainsKey(item.Key))
            //    {
            //        _bonuses[item.Key].Dispose();
            //        _bonuses.Remove(item.Key);
            //    }    
            //}

            //foreach (var item in newBonuses)
            //{
            //    if (!_bonuses.ContainsKey(item.Key))
            //    {
            //        var view = _view.AdjacencyContainer.Spawn<IAdjacencyTextView>(_view.AdjacencyPrefab);
            //        _bonuses[item.Key] = view;
            //    }
            //}

            //foreach (var item in newBonuses)
            //{
            //    var text =_bonuses[item.Key].Text;
            //    text.Value = $"{item.Value}";
            //    text.Position = _fieldService.GetWorldPosition(item.Key);
            //}

            //_buildingModeService.SetHighlight(_bonuses.Keys.ToArray().AsReadOnly());
        }

        private void OnExitTap()
        {
            ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

    }
}
