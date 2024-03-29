﻿using System;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases
{
    public class DisposeTests
    {
        public static void TestDisposables()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            var dislosables = Disposable.GetListOfUndisposed();
            if (dislosables.Count > 0)
            {
                Assert.Fail($"Some instances {dislosables.Count} are not disposed: {dislosables.Last()}");
                Disposable.ClearUndisopsed();
            }
        }

    }
}
