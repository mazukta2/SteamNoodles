using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
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

        public bool IsLoaded { get; private set; }
        public GameLevel Model { get; }

        private List<IView> _views = new List<IView>();

        public LevelView(GameLevel model, IControls controls)
        {
            Model = model;
        }

        public void Dispose()
        {
            IScreenManagerPresenter.Default = null;
            IGhostManagerPresenter.Default = null;
            IPointPieceSpawnerPresenter.Default = null;

            OnDispose();

            if (_views.Count > 0)
                throw new Exception("View presenters are not destroyed");
        }

        public T FindView<T>() where T : IView
        {
            return _views.ToList().OfType<T>().FirstOrDefault();
        }

        public IReadOnlyCollection<T> FindViews<T>() where T : IView
        {
            return _views.ToList().OfType<T>().AsReadOnly();
        }

        public void Remove(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Remove(view);
        }

        public void Add(IView view)
        {
            if (view == null)
                throw new Exception("View can't be null");
            _views.Add(view);
        }

        public void FinishLoading()
        {
            InitViews();

            IsLoaded = true;
            OnLoaded();
        }

        private void InitViews()
        {
            var initing = _views.OfType<IViewWithDefaultPresenter>().ToList();
            
            IScreenManagerPresenter.Default = SetPresenterForViewAndGetFirst<IScreenManagerView, ScreenManagerPresenter>();
            IGhostManagerPresenter.Default = SetPresenterForViewAndGetFirst<IGhostManagerView, GhostManagerPresenter>();
            IPointPieceSpawnerPresenter.Default = SetPresenterForViewAndGetFirst<IPointPieceSpawner, PointPieceSpawnerPresenter>();

            while (initing.Count > 0)
            {
                var view = initing.First();
                initing.Remove(view);
                view.Init();
            }

            IEnumerable<TView> SetPresenterForView<TView, TPresenter>()
                where TView : IViewWithPresenter, IViewWithDefaultPresenter
                where TPresenter : class, IPresenter
            {
                var views = _views.OfType<TView>().ToList();
                foreach (var item in views)
                {
                    initing.Remove(item);
                    item.Init();
                }
                return views;
            }

            TPresenter SetPresenterForViewAndGetFirst<TView, TPresenter>()
                where TView : IViewWithPresenter, IViewWithDefaultPresenter
                where TPresenter : class, IPresenter
            {
                var items = SetPresenterForView<TView, TPresenter>();
                if (items.Count() > 1)
                    throw new Exception("More then one singleton view");

                if (items.Count() == 0)
                    return null;

                return (TPresenter)items.First().Presenter;
            }

        }
    }
}
