using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using System;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
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
