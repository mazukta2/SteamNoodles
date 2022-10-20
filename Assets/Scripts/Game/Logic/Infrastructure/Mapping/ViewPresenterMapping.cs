using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.TradeLevel;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.StartMenuLevel;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Levels.StartMenuLevels;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Mapping
{
    public class ViewPresenterMapping
    {
        private IViews _collection;
        private Dictionary<Type, Type> _viewToPresenter = new Dictionary<Type, Type>();

        public ViewPresenterMapping(IViews collection)
        {
            _collection = collection;
            MapEverything();
        }

        public void Init()
        {
            var toInit = _collection.FindViews<IView>().ToList();


            while (toInit.Count > 0)
            {
                var view = toInit.First();
                toInit.Remove(view);

                TryConnect(view.GetType(), view);

                foreach (var interfaceType in view.GetType().GetInterfaces())
                {
                    TryConnect(interfaceType, view);
                }
            }
            

            void TryConnect(Type viewType, IView view)
            {
                if (_viewToPresenter.ContainsKey(viewType))
                {
                    var presenterType = _viewToPresenter[viewType];
                    var ctor = presenterType.GetConstructor(new[] { viewType });

                    if (ctor == null)
                        throw new Exception("No constructor");

                    var istance = ctor.Invoke(new object[] { view });

                    if (istance is IPresenter)
                        Connect(view, (IPresenter)istance);
                    else
                        throw new Exception("Is not presenter");

                }
            }
        }

        private void MapEverything()
        {
            Map<IExitGameButtonView, ExitButtonPresenter>();
            Map<IStartNewGameView, StartGameButtonPresenter>();
            Map<IDialogView, DialogPresenter>();
        }

        public void Map<TView, TPresenter>() where TView : IView where TPresenter : IPresenter
        {
            _viewToPresenter.Add(typeof(TView), typeof(TPresenter));
        }

        private void Connect(IView view, IPresenter presenter)
        {
            view.OnDispose += presenter.Dispose;
            presenter.OnDispose += view.Dispose;
        }
    }
}

