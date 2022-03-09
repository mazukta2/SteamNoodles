using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.ViewPresenters;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevel : IDisposable
    {
        event Action OnDispose;
        GameLevel Model { get; }
        void Remove(ViewPresenter viewPresenter);
        void Add(ViewPresenter viewPresenter);
    }
}
