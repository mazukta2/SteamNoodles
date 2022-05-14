using Game.Assets.Scripts.Game.Logic.Common.Math;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IWorldText
    {
        string Value { get; set; }
        FloatPoint3D Position { get; set; }
    }
}
