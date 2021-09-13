using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandViewModel
    {
        private PlayerHand _model;
        private IHandView _view;
        private PlacementViewModel _placement;
        private List<HandConstructionViewModel> _list = new List<HandConstructionViewModel>();
        private HistoryReader _historyReader;

        public HandViewModel(PlayerHand model, IHandView view, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            _view = view;
            _placement = placement;

            _historyReader = new HistoryReader(_model.History);
            _historyReader.Subscribe<SchemeAddedToHandEvent>(ScnemeAddedHandle).Update();
        }

        public HandConstructionViewModel[] GetConstructions()
        {
            return _list.ToArray();
        }

        private void ScnemeAddedHandle(SchemeAddedToHandEvent obj)
        {
            _list.Add(new HandConstructionViewModel(obj.Construction, _view.CreateConstrcution(), OnSchemeClick));
        }

        private void OnSchemeClick(ConstructionScheme obj)
        {
            _placement.SetGhost(obj);
        }
    }
}
