using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Tests.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Environment
{
    public class LevelInTests : ILevel
    {
        public event Action OnLoadedUpdate = delegate { };

        public event Action OnDispose = delegate { };
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
            _testLevelsManager.Unload();

            OnDispose();
            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public T FindView<T>() where T : View
        {
            return _views.OfType<T>().FirstOrDefault();
        }

        public void Remove(View viewPresenter)
        {
            _views.Remove(viewPresenter);
        }

        public void Add(View viewPresenter)
        {
            _views.Add(viewPresenter);
        }
    }
}
