using Game.Assets.Scripts.Tests.Environment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Mocks
{
    public abstract class MockPrefab<T> 
    {
        public abstract void Spawn(LevelInTests level, T s);
    }
}
