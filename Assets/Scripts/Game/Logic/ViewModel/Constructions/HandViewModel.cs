using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
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

            foreach (var item in model.CurrentSchemes)
                ScnemeAddedHandle(item);

            _model.OnAdded += ScnemeAddedHandle;
            _model.OnRemoved += ScnemeRemovedHandle;
        }

        public void Destroy()
        {
            _model.OnAdded -= ScnemeAddedHandle;
            _model.OnRemoved -= ScnemeRemovedHandle;

            foreach (var item in _list)
                item.Destroy();
            View.Destroy();
            IsDestoyed = true;
        }

        public IHandView View { get; private set; }
        public bool IsDestoyed { get; private set; }

        public HandConstructionViewModel[] GetSchemes()
        {
            return _list.ToArray();
        }

        public void Add(IConstructionPrototype building)
        {
            _model.Add(building);
        }

        private void ScnemeAddedHandle(ConstructionScheme obj)
        {
            _list.Add(new HandConstructionViewModel(obj, View.CreateConstruction(), OnSchemeClick));
        }

        private void ScnemeRemovedHandle(ConstructionScheme obj)
        {
            var scheme = _list.First(x => x.Scheme == obj);
            _list.Remove(scheme);
            scheme.Destroy();
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
