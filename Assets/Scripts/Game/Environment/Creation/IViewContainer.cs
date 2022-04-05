using Game.Assets.Scripts.Game.Logic.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Environment.Creation
{
    public interface IViewContainer
    {
        T Spawn<T>(IViewPrefab prefab) where T : IView;
        void Clear();
    }
}
