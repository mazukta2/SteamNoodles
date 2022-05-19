using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Managing
{
    public class ViewsInitializer 
    {
        private IViewsCollection _collection;

        public ViewsInitializer(IViewsCollection collection) 
        {
            _collection = collection;
        }

        public void Init()
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
