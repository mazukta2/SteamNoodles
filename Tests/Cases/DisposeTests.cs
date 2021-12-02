using Assets.Scripts.Logic;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Tests.Controllers;
using Game.Tests.Mocks.Prototypes.Levels;
using Game.Tests.Mocks.Views.Game;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tests.Assets.Scripts.Game.Logic.Presenters;
using Tests.Mocks.Prototypes.Levels;

namespace Game.Tests.Cases
{
    public class DisposeTests
    {
        public static void TestDisposables()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var dislosables = Disposable.GetListOfUndisposed();
            if (dislosables.Count > 0)
                Assert.Fail($"Some instances {dislosables.Count} are not disposed: {dislosables.Last()}");
        }

    }
}
