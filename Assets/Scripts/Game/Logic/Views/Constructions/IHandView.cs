using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IHandView : IView
    {
        public DisposableViewListKeeper<IHandConstructionView> Cards { get; }
    }
}
