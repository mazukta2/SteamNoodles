using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Creation
{
    public interface IViewContainer : IViewsCollection
    {
        T Spawn<T>(IViewPrefab prefab) where T : class, IView;
        void Spawn(IViewPrefab prefab);
        void Clear();
    }
}
