using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Views.Common.Creation
{

    public class ContainerViewMock : View, IViewContainer
    {
        public IReadOnlyCollection<IView> Views => _collection.Views;
        private ViewsCollection _collection = new ViewsCollection();

        public ContainerViewMock(IViewsCollection level) : base(level)
        {
        }

        protected override void DisposeInner()
        {
            _collection.Dispose();
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public T Spawn<T>(IViewPrefab prefab) where T : class, IView
        {
            if (prefab is ViewCollectionPrefabMock viewPrefabMock)
            {
                viewPrefabMock.Fill(_collection);
                return FindViews<T>().Last();
            }
            throw new Exception("Uknown prefab type: " + prefab);
        }

        public void Spawn(IViewPrefab prefab, GameVector3 position)
        {
            Spawn(prefab);
        }

        public void Spawn(IViewPrefab prefab)
        {
            if (prefab is ViewCollectionPrefabMock viewPrefabMock)
            {
                viewPrefabMock.Fill(_collection);
                return;
            }
            throw new Exception("Uknown prefab type: " + prefab);
        }

        public T FindView<T>(bool recursively = true) where T : IView
        {
            return _collection.FindView<T>(recursively);
        }

        public IReadOnlyCollection<T> FindViews<T>(bool recursively = true) where T : IView
        {
            return _collection.FindViews<T>(recursively);
        }

        public void Remove(IView view)
        {
            _collection.Remove(view);
        }

        public void Add(IView view)
        {
            _collection.Add(view);
        }
    }
}
