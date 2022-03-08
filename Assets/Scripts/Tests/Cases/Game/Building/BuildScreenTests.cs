using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Unity.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Environment.Assets;
using Game.Assets.Scripts.Tests.Mocks.Prefabs.Screens;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Building
{
    public class BuildScreenTests
    {
        [Test]
        public void FromMainScreenToBuildScreen()
        {
            //var assets = new ScreenAssetsInTests();
            //assets.AddPrototype(new MainScreenMock());
            //assets.AddPrototype(new BuildScreenMock());

            //var managerView = new ScreenManagerView();
            //var screenManagerPresenter = new ScreenManagerPresenter(managerView, assets);

            //Assert.IsTrue(managerView.Screen.Has<MainScreenView>());
            //Assert.IsFalse(managerView.Screen.Has<BuildScreenView>());

            //var mainScreen = managerView.Screen.Get<MainScreenView>().First();
            //mainScreen.BuildButton.Click();

            //Assert.IsTrue(managerView.Screen.Has<BuildScreenView>());
            //Assert.IsFalse(managerView.Screen.Has<MainScreenView>());

            //managerView.Screen.Get<BuildScreenView>().First().CloseButton.Click();

            //Assert.IsTrue(managerView.Screen.Has<MainScreenView>());
            //Assert.IsFalse(managerView.Screen.Has<BuildScreenView>());

            //assets.Dispose();
            //managerView.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
