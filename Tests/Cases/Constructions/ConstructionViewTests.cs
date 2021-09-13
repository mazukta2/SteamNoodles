using NUnit.Framework;
using System.Drawing;
using System.Linq;
using System.Numerics;
using Tests.Tests.Shortcuts;

namespace Tests.Tests.Cases.Constructions
{
    public class ConstructionViewTests
    {
        [Test]
        public void IsIconSettedInHand()
        {
            var (level, viewLevel) = new LevelShortcuts().LoadLevel();
            var item = viewLevel.Screen.Hand.GetConstructions().First();
        }

    }
}
