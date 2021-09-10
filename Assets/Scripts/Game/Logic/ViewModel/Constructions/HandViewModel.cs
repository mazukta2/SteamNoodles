using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandViewModel
    {
        private PlayerHand _model;
        private PlacementViewModel _placement;
        private List<HandConstructionViewModel> _list = new List<HandConstructionViewModel>();
        private HistoryReader _historyReader;

        public HandViewModel(PlayerHand model, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
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
            _list.Add(new HandConstructionViewModel(obj.Construction, OnSchemeClick));
        }

        private void OnSchemeClick(ConstructionScheme obj)
        {
            _placement.SetGhost(obj);
        }
    }
}
