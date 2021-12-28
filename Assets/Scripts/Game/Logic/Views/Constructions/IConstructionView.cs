using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System.Numerics;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IConstructionView : IView
    {
        void SetPosition(Vector2 pos);
        Vector2 GetPosition();
        IVisual GetImage();
        void SetImage(IVisual image);
    }
}
