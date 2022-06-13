using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Models.Constructions
{
    public class HandModelMock : Disposable, IHandModel
    {
        public event Action<IConstructionHandModel> OnAdded = delegate { };
        public event Action<IConstructionHandModel> OnRemoved = delegate { };

        private List<IConstructionHandModel> _list = new List<IConstructionHandModel>();

        public HandModelMock()
        {
        }

        protected override void DisposeInner()
        {
        }

        public IReadOnlyCollection<IConstructionHandModel> GetCards()
        {
            return _list.AsReadOnly();
        }

        public void Add(IConstructionHandModel card)
        {
            _list.Add(card);
            OnAdded(card);
        }

        public void Remove(IConstructionHandModel card)
        {
            _list.Remove(card);
            OnRemoved(card);
        }
    }
}
