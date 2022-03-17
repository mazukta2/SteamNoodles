using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Views;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevel : IDisposable
    {
        event Action OnDispose;
        IGameEngine Engine { get; }
        GameLevel Model { get; }
        Services Services { get; }
        void Remove(View viewPresenter);
        void Add(View viewPresenter);
    }
}
