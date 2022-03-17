using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Tests.Controllers;
using NUnit.Framework.Constraints;
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
        public Services Services { get; } = new Services();
        public IGameEngine Engine { get; }

        private bool _loaded;
        private LevelsManagerInTests _testLevelsManager;
        private List<View> _views = new List<View>();

        public LevelInTests(LevelsManagerInTests testLevelsManager, IGameEngine engine, GameLevel gameLevel)
        {
            _testLevelsManager = testLevelsManager;
            Model = gameLevel;
            Engine = engine;
        }

        public void Dispose()
        {
            Services.Dispose();
            _testLevelsManager.Unload();

            OnDispose();
            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public T FindView<T>() where T : View
        {
            return _views.OfType<T>().FirstOrDefault();
        }

        public IReadOnlyCollection<T> FindViews<T>() where T : View
        {
            return _views.OfType<T>().AsReadOnly();
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
