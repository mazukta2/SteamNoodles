using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class GhostPointPresenter : BasePresenter<IGhostPointsView>
    {
        private IGhostPointsView _view;
        private readonly GhostPresentationRepository _ghost;
        private readonly Field _field;
        private readonly IDataCollectionProvider<ConstructionPresenterData> _constructions;

        //private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public GhostPointPresenter(IGhostPointsView view) : this(
                view,
                IPresenterServices.Default?.Get<GhostPresentationRepository>(),
                IPresenterServices.Default?.Get<IDataCollectionProviderService<ConstructionPresenterData>>().Get(),
                IPresenterServices.Default?.Get<ISingletonDatabase<Field>>().Get())
        {
        }

        public GhostPointPresenter(IGhostPointsView view,
            GhostPresentationRepository ghost,
            IDataCollectionProvider<ConstructionPresenterData> constructions,
            Field field) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _field = field ?? throw new ArgumentNullException(nameof(field));

            //_ghost.OnEvent += HandleOnEvent;
            // _ghost.OnAdded += UpdatePoints;
            // _ghost.OnRemoved += UpdatePoints;
            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            //_ghost.OnEvent -= HandleOnEvent;
            // _ghost.OnAdded -= UpdatePoints;
            // _ghost.OnRemoved -= UpdatePoints;
        }
        
        private void HandleOnEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent && obj is not GhostPointsChangedEvent)
                return;
            
            UpdatePoints();
        }

        public void UpdatePoints()
        {
            // if (_ghost.Has())
            // {
            //     var ghost = _ghost.Get();
            //     _view.Points.Value = ghost.GetPoints().AsString();
            //     _view.Points.Position = ghost.GetWorldPosition();
            // }
            // else
            // {
            //     _view.Points.Value = "";
            // }

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
            //    text.Position = _Field.GetWorldPosition(item.Key);
            //}

            //_buildingModeService.SetHighlight(_bonuses.Keys.ToArray().AsReadOnly());
        }

        private void OnExitTap()
        {
            //ScreenManagerPresenter.Default.Open<IMainScreenView>(x => new MainScreenPresenter(x));
        }

    }
}
