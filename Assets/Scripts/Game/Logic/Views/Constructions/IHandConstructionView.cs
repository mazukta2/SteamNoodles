using Tests.Assets.Scripts.Game.Logic.Views.Common;

namespace Tests.Assets.Scripts.Game.Logic.Views.Constructions
{
    public interface IHandConstructionView
    {
        void SetIcon(ISprite icon);
        ISprite GetIcon();
    }
}
