using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class LevelController : Disposable
    {
        public ViewsCollection Collection => _collection;

        private ViewsCollection _collection = new ViewsCollection();
        private ViewsInitializer _initializer;
        private LevelDefinition _level;

        public LevelController(LevelDefinition levelDefinition)
        {
            _level = levelDefinition;
        }

        protected override void DisposeInner()
        {
            if (_initializer != null)
                _initializer.Dispose();

            if (_collection != null)
                _collection.Dispose();
        }

        public void Initialize()
        {
            _initializer = new ViewsInitializer(_collection);
        }

        public void Start()
        {
            _level.Starter.Start();
        }
    }
}
