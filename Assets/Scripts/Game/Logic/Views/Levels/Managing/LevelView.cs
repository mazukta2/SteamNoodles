using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
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
            var initing = _views.OfType<IViewWithAutoInit>().ToList();
            IScreenManagerPresenter.Default = InitDefaultValue<IScreenManagerView, ScreenManagerPresenter>();
            IGhostManagerPresenter.Default = InitDefaultValue<IGhostManagerView, GhostManagerPresenter>();
            IPointPieceSpawnerPresenter.Default = InitDefaultValue<IPointPieceSpawner, PointPieceSpawnerPresenter>();

            while (initing.Count > 0)
                InitView(initing.First());

            IsLoaded = true;
            OnLoaded();

            void InitView(IViewWithAutoInit view)
            {
                initing.Remove(view);
                view.Init();
            }

            TPresenter InitDefaultValue<TView, TPresenter>() 
                where TView : IViewWithPresenter, IViewWithAutoInit, IViewWithGenericPresenter<TPresenter>
                where TPresenter : class, IPresenter
            {
                var view = _views.OfType<TView>().FirstOrDefault();
                if (view != null)
                {
                    InitView(view);
                    return view.Presenter;
                }
                return null;
            }
        }
    }
}
