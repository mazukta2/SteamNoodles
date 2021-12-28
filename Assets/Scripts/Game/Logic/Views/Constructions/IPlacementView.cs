using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IPlacementView : IView
    {
        DisposableViewKeeper<IGhostConstructionView> Ghost { get; }
        DisposableViewListKeeper<ICellView> Cells { get; }
        DisposableViewListKeeper<IConstructionView> Constructions { get; }

        void SetClick(Action<Vector2> onClick);
        void Click(Vector2 vector2);
    }
}
