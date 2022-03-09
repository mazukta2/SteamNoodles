using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
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
        private List<ViewPresenter> _views = new List<ViewPresenter>();

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

        public T FindViewPresenter<T>() where T : ViewPresenter
        {
            return _views.OfType<T>().FirstOrDefault();
        }

        public void Remove(ViewPresenter viewPresenter)
        {
            _views.Remove(viewPresenter);
        }

        public void Add(ViewPresenter viewPresenter)
        {
            _views.Add(viewPresenter);
        }
    }
}
