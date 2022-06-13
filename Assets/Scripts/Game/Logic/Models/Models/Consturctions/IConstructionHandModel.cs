using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IConstructionHandModel : IDisposable
    {
        Uid Id { get; }

        void ConnectPresenter(IHandConstructionView view);
    }
}
