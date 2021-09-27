using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Tests.Assets.Scripts.Game.Logic.Views
{
    public interface IConstructionView : IView
    {
        void SetPosition(Vector2 pos);
        Vector2 GetPosition();
        IVisual GetImage();
        void SetImage(IVisual image);
    }
}
