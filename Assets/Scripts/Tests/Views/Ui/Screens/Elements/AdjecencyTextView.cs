using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Elements;
using Game.Assets.Scripts.Tests.Views.Common;

namespace Game.Assets.Scripts.Tests.Views.Ui.Screens.Elements
{
    public class AdjecencyTextView : View, IAdjacencyTextView
    {
        public IWorldText Text { get; } = new UiWorldText();

        public AdjecencyTextView(IViews level) : base(level)
        {
        }
    }
}
