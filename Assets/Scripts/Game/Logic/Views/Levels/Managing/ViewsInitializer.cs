using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Collections;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class ViewsInitializer : Disposable
    {
        private IViewsCollection _collection;

        public ViewsInitializer(IViewsCollection collection) 
        {
            _collection = collection;
            InitViews();
        }

        protected override void DisposeInner()
        {
        }

        private void InitViews()
        {
            var initing = _collection.FindViews<IViewWithDefaultPresenter>().ToList();

            SetDefaitPresenterForViews<IScreenManagerView>();
            SetDefaitPresenterForViews<IHandView>();
            SetDefaitPresenterForViews<IGhostManagerView>();
            SetDefaitPresenterForViews<IPointPieceSpawnerView>();

            while (initing.Count > 0)
            {
                var view = initing.First();
                initing.Remove(view);
                view.InitDefaultPresenter();
            }

            IEnumerable<TView> SetDefaitPresenterForViews<TView>()
                where TView : IViewWithPresenter, IViewWithDefaultPresenter
            {
                var views = _collection.FindViews<TView>().ToList();
                foreach (var item in views)
                {
                    initing.Remove(item);
                    item.InitDefaultPresenter();
                }
                return views;
            }
        }
    }
}
