using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class LevelView
    {
        public event Action OnDispose = delegate { };
        public event Action OnLoaded = delegate { };

        public LevelServices Services { get; } = new LevelServices();
        public bool IsLoaded { get; private set; }
        public GameLevel Model { get; }
        public IGameEngine Engine { get; }

        private List<IView> _views = new List<IView>();

        public LevelView(IGameEngine engine, GameLevel model)
        {
            Model = model;
            Engine = engine;
        }

        public void Dispose()
        {
            Services.Dispose();
            OnDispose();

            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public T FindView<T>() where T : IView
        {
            return _views.OfType<T>().FirstOrDefault();
        }

        public IReadOnlyCollection<T> FindViews<T>() where T : IView
        {
            return _views.OfType<T>().AsReadOnly();
        }

        public void Remove(IView view)
        {
            _views.Remove(view);
        }

        public void Add(IView view)
        {
            _views.Add(view);
        }

        public void FinishLoading()
        {
            var initing = _views.OfType<IViewWithAutoInit>().ToList();

            var screenManager = _views.OfType<IScreenManagerView>().FirstOrDefault();
            if (screenManager != null)
            {
                InitView(screenManager);
                Services.Add(new ScreenManagerService(screenManager.Presenter));
            }

            var ghostManager = _views.OfType<IGhostManagerView>().FirstOrDefault();
            if (ghostManager != null)
            {
                InitView(ghostManager);
                Services.Add(new GhostManagerService(ghostManager.Presenter));
            }

            while (initing.Count > 0)
                InitView(initing.First());

            IsLoaded = true;
            OnLoaded();

            void InitView(IViewWithAutoInit view)
            {
                initing.Remove(view);
                view.Init();
            }
        }
    }
}
