using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Models.Events;
using Tests.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions
{
    public class HandPresenter : Disposable
    {
        private PlayerHand _model;
        private PlacementPresenter _placement;
        private List<HandConstructionPresenter> _list = new List<HandConstructionPresenter>();
        private HistoryReader _historyReader;

        public HandPresenter(PlayerHand model, IHandView view, PlacementPresenter placement)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            View = view;
            _placement = placement;

            foreach (var item in model.Cards)
                ScnemeAddedHandle(item);

            _model.OnAdded += ScnemeAddedHandle;
            _model.OnRemoved += ScnemeRemovedHandle;
        }


        protected override void DisposeInner()
        {
            _model.OnAdded -= ScnemeAddedHandle;
            _model.OnRemoved -= ScnemeRemovedHandle;

            foreach (var item in _list)
                item.Dispose();
            View.Dispose();
        }

        public IHandView View { get; private set; }

        public HandConstructionPresenter[] GetSchemes()
        {
            return _list.ToArray();
        }

        public void Add(IConstructionSettings building)
        {
            _model.Add(building);
        }

        private void ScnemeAddedHandle(ConstructionCard obj)
        {
            _list.Add(new HandConstructionPresenter(obj, View.CreateConstruction(), OnSchemeClick));
        }

        private void ScnemeRemovedHandle(ConstructionCard obj)
        {
            var scheme = _list.First(x => x.Scheme == obj);
            _list.Remove(scheme);
            scheme.Dispose();
        }

        private void OnSchemeClick(ConstructionCard obj)
        {
            if (_placement.Ghost == null)
                _placement.SetGhost(obj);
            else
                _placement.ClearGhost();
        }
    }
}
