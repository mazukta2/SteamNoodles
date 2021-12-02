using Assets.Scripts.Logic.Prototypes.Levels;
using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Assets.Scripts.Game.Logic.Presenters.Levels;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Constructions
{
    public class HandPresenter : Disposable
    {
        private readonly PlayerHand _model;
        private readonly PlacementPresenter _placement;
        private readonly List<HandConstructionPresenter> _list = new List<HandConstructionPresenter>();

        public HandPresenter(PlayerHand model, IHandView view, PlacementPresenter placement)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
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

            _placement.ClearGhost();
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
            _list.Add(new HandConstructionPresenter(obj, View.CreateConstruction(), OnCardClick));
        }

        private void ScnemeRemovedHandle(ConstructionCard obj)
        {
            var scheme = _list.First(x => x.Scheme == obj);
            _list.Remove(scheme);
            scheme.Dispose();
        }

        private void OnCardClick(ConstructionCard card)
        {
            if (_placement.Ghost == null)
                _placement.SetGhost(card);
            else
                _placement.ClearGhost();
        }
    }
}
