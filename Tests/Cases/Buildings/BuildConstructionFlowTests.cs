using NUnit.Framework;
using Tests.Tests.Shortcuts;

namespace Tests.Tests.Cases.Buildings
{
    public class BuildConstructionFlowTests
    {
        [Test]
        public void IsHandViewModelExistingOnLevelLoaded()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();
            var screen = viewLevel.Screen;
            Assert.IsNotNull(screen.Hand);
        }
    }
}
