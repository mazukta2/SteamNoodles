using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using System;
using System.Collections.Generic;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.ViewModel.Constructions
{
    public class HandViewModel : IViewModel
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
            _historyReader.Subscribe<SchemeAddedToHandEvent>(ScnemeAddedHandle);
        }

        public void Destroy()
        {
            foreach (var item in _list)
                item.Destroy();
            View.Destroy();
            IsDestoyed = true;
        }

        public IHandView View { get; private set; }
        public bool IsDestoyed { get; private set; }

        public HandConstructionViewModel[] GetConstructions()
        {
            return _list.ToArray();
        }

        public void Add(IConstructionPrototype building)
        {
            _model.Add(building);
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
