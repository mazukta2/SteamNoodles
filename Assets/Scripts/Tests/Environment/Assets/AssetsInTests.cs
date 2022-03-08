using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Environment.Engine.Assets;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Tests.Environment.Assets;

namespace Game.Tests.Controllers
{
    public class AssetsInTests : Disposable, IAssets
    {
        public ScreenAssetsInTests Screens { get; } = new ScreenAssetsInTests();
        IScreenAssets IAssets.Screens => Screens;

        protected override void DisposeInner()
        {
            Screens.Dispose();
        }
    }
}
