using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Environment
{
    /*
    public class TestLevel : ILevel
    {
        public event Action OnLoadedUpdate = delegate { };

        private List<GameObject> _gameObjects = new List<GameObject>();
        private bool _loaded;
        private TestLevelsManager _testLevelsManager;

        public TestLevel(TestLevelsManager testLevelsManager)
        {
            _testLevelsManager = testLevelsManager;
        }

        public bool Loaded { get => _loaded; internal set { _loaded = value; OnLoadedUpdate(); } }

        public void Dispose()
        {
            foreach (var item in _gameObjects.ToList())
            {
                GameObject.DestroyImmediate(item);
            }
            _testLevelsManager.Unload();
        }

        public void Add(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Remove(GameObject gameObject)
        {
            _gameObjects.Remove(gameObject);
        }

        public T FindObject<T>() where T : MonoBehaviour
        {
            foreach (var item in _gameObjects)
            {
                var c = item.GetComponent<T>();
                if (c != null)
                    return c;
            }
            return null;
        }
    }
    */
}
