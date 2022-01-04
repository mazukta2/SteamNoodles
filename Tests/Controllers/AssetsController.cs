using Game.Assets.Scripts.Game.Logic.Controllers.Level;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Tests.Mocks.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Tests.Controllers
{
    public class AssetsController : IAssetsController
    {
        private Dictionary<string, object> _list = new Dictionary<string, object>();
        public void Add(string id, object item)
        {
            _list.Add(id, item);
        }

        public ISprite GetSprite(string path)
        {
            return (ISprite)_list[path];
        }

        public IVisual GetVisual(string path)
        {
            return (IVisual)_list[path];
        }
    }
}
