using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IHandModel : IDisposable
    {
        event Action<IConstructionHandModel> OnAdded;
        event Action<IConstructionHandModel> OnRemoved;

        IReadOnlyCollection<IConstructionHandModel> GetCards();
    }
}
