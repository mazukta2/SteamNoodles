using NUnit.Framework;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Tests.Tests.Shortcuts;

namespace Tests.Tests.Cases.Constructions
{
    public class BuildConstructionFlowTests
    {
        [Test]
        public void IsHandViewModelExistingOnLevelLoaded()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();
            Assert.IsNotNull(level.Hand);
            Assert.IsNotNull(viewLevel.Screen.Hand);
        }

        [Test]
        public void IsHandHaveItems()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();
            var items = viewLevel.Screen.Hand.GetConstructions();
            Assert.IsTrue(items.Length > 0);
        }

        [Test]
        public void IsClickToConstructionEnterGhoustMode()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();

            Assert.IsNull(viewLevel.Placement.Ghost);

            var construction = viewLevel.Screen.Hand.GetConstructions().First();
            construction.OnClick();

            Assert.IsNotNull(viewLevel.Placement.Ghost);
        }

        [Test]
        public void PointToPlacementAndBuild()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();
            var construction = viewLevel.Screen.Hand.GetConstructions().First();
            construction.OnClick();

            viewLevel.Placement.OnClick(new Vector2(0f, 0f));

            //viewLevel.Placement.Con
        }
    }
}
