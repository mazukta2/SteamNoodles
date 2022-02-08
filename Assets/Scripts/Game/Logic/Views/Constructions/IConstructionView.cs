using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IConstructionView : IView
    {
        void SetPosition(FloatPoint pos);
        FloatPoint GetPosition();
        IVisual GetImage();
        void SetImage(IVisual image);
    }
}
