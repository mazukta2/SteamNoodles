using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevel : IDisposable
    {
        event Action OnDispose;
        GameLevel Model { get; }
        void Remove(View viewPresenter);
        void Add(View viewPresenter);
    }
}
