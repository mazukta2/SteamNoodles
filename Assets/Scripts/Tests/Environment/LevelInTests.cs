using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelInTests : ILevel
    {
        public event Action OnLoadedUpdate = delegate { };
        public bool Loaded { get => _loaded; internal set { _loaded = value; OnLoadedUpdate(); } }

        public GameLevel Model { get; private set; }

        private bool _loaded;
        private LevelsManagerInTests _testLevelsManager;
        private List<View> _views = new List<View>();

        public LevelInTests(LevelsManagerInTests testLevelsManager, GameLevel gameLevel)
        {
            _testLevelsManager = testLevelsManager;
            Model = gameLevel;
        }

        public void Dispose()
        {
            foreach (var item in _views)
                item.Dispose();

            _testLevelsManager.Unload();
        }

        public T FindObject<T>() where T : View
        {
            return _views.OfType<T>().FirstOrDefault();
        }

        public T Add<T>(T view) where T : View
        {
            _views.Add(view);
            view.Awake(this);
            return view;
        }
    }
}
