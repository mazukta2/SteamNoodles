using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using Tests.Mocks.Prototypes.Levels;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandViewModel
    {
        private PlayerHand _model;
        private PlacementViewModel _placement;
        private List<HandConstructionViewModel> _list = new List<HandConstructionViewModel>();
        private HistoryReader _historyReader;


        public HandViewModel(PlayerHand model, IHandView view, PlacementViewModel placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
            _placement = placement;

            _historyReader = new HistoryReader(_model.History);
            _historyReader.Subscribe<SchemeAddedToHandEvent>(ScnemeAddedHandle).Update();
        }

        public IHandView View { get; private set; }

        public HandConstructionViewModel[] GetConstructions()
        {
            _historyReader.Update();
            return _list.ToArray();
        }

        public void Add(BasicBuildingPrototype building)
        {
            _model.Add(building);
            _historyReader.Update();
        }

        private void ScnemeAddedHandle(SchemeAddedToHandEvent obj)
        {
            _list.Add(new HandConstructionViewModel(obj.Construction, View.CreateConstruction(), OnSchemeClick));
        }

        private void OnSchemeClick(ConstructionScheme obj)
        {
            if (_placement.Ghost == null)
                _placement.SetGhost(obj);
            else
                _placement.ClearGhost();
        }
    }
}
