using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IPlacementView : IView
    {
        IGhostConstructionView CreateGhost();
        void SetClick(Action<Vector2> onClick);
        void Click(Vector2 vector2);
        IConstructionView CreateConstrcution();
        ICellView CreateCell();
    }
}
