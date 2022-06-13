using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class HandModel : Disposable, IHandModel
    {
        public event Action<IConstructionHandModel> OnAdded = delegate { };
        public event Action<IConstructionHandModel> OnRemoved = delegate { };

        private List<IConstructionHandModel> _list = new List<IConstructionHandModel>();

        public HandModel()
        {
        }

        protected override void DisposeInner()
        {
            foreach (var card in _list)
                card.Dispose();
        }

        public IReadOnlyCollection<IConstructionHandModel> GetCards()
        {
            return _list.AsReadOnly();
        }

        public IConstructionHandModel Get(Uid id)
        {
            return _list.First(x => x.Id == id);
        }

        public void Add(IConstructionHandModel card)
        {
            _list.Add(card);
            OnAdded(card);
        }

        public void Remove(Uid card)
        {
            var model = Get(card);
            model.Dispose();
            _list.Remove(model);
            OnRemoved(model);
        }

    }
}
