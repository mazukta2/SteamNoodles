using Game.Assets.Scripts.Game.Logic.Common.Math;
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

        void SetClick(Action<FloatPoint> onClick);
        void Click(FloatPoint vector2);
    }
}
