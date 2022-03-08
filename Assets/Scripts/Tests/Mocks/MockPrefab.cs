using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Mocks
{
    public abstract class MockPrefab<T> 
    {
        public abstract void Mock(List<IDisposable> disposables, T s);
    }
}
