using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class GhostPointPresenter : BasePresenter<IGhostPointsView>
    {
        private IGhostPointsView _view;
        private readonly ISingleQuery<ConstructionGhost> _ghost;
        private readonly ConstructionsService _constructionsService;
        private readonly Field _field;

        //private Dictionary<Construction, IAdjacencyTextView> _bonuses = new Dictionary<Construction, IAdjacencyTextView>();

        public GhostPointPresenter(IGhostPointsView view) : this(
                view,
                IPresenterServices.Default?.Get<ISingletonRepository<ConstructionGhost>>().AsQuery(),
                IPresenterServices.Default?.Get<ConstructionsService>(),
                IPresenterServices.Default?.Get<ISingletonRepository<Field>>().Get())
        {
        }

        public GhostPointPresenter(IGhostPointsView view,
            ISingleQuery<ConstructionGhost> ghost,
            ConstructionsService constructionsService,
            Field field) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _field = field ?? throw new ArgumentNullException(nameof(Field));

            _ghost.OnEvent += HandleOnEvent;
            _ghost.OnAdded += UpdatePoints;
            _ghost.OnRemoved += UpdatePoints;
            UpdatePoints();
        }

        protected override void DisposeInner()
        {
            _ghost.Dispose();
            _ghost.OnEvent -= HandleOnEvent;
            _ghost.OnAdded -= UpdatePoints;
            _ghost.OnRemoved -= UpdatePoints;
        }
        
        private void HandleOnEvent(IModelEvent obj)
        {
            if (obj is not GhostMovedEvent && obj is not GhostPointsChangedEvent)
                return;
            
            UpdatePoints();
        }

        public void UpdatePoints()
        {
            if (_ghost.Has())
            {
                var ghost = _ghost.Get();
                var points = _constructionsService.GetPoints(ghost.Card, ghost.Position, ghost.Rotation);

                var worldPosition = ghost.Position.GetWorldPosition(
                    ghost.Card.Scheme.Placement.GetRect(ghost.Rotation));

                _view.Points.Value = points.AsString();
                _view.Points.Position = worldPosition;
            }
            else
            {
                _view.Points.Value = "";
            }

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
