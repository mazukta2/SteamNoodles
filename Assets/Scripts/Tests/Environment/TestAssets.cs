namespace Game.Tests.Controllers
{
    /*
    public class TestAssets : IAssets
    {
        private Dictionary<string, object> _list = new Dictionary<string, object>();


        public void Add(string id, object item)
        {
            _list.Add(id, item);
        }

        public void Dispose()
        {
            foreach (var item in _list)
            {
                if (item.Value is GameObject go)
                {
                    GameObject.DestroyImmediate(go);
                }
            }
        }

        public GameObjectResource GetScreen(string name)
        {
            return new ScreenAccess(this, name);
        }

        private class ScreenAccess : GameObjectResource
        {
            private TestAssets _controller;
            private string _name;

            public ScreenAccess(TestAssets controller, string name)
            {
                _controller = controller;
                _name = name;
            }

            public override GameObject Create(Transform transform)
            {
                if (!_controller._list.ContainsKey(_name))
                    throw new System.Exception("Cant find resource : " + _name);

                var go = (GameObject)_controller._list[_name];
                return GameObject.Instantiate(go, transform);
            }
        }

        public ScreenResource<T> GetScreen<T>() where T : IScreenView
        {
            throw new System.NotImplementedException();
        }
    }
    */
}
