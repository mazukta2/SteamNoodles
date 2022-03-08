using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public interface ILevel : IDisposable
    {
        T FindObject<T>() where T : View;
        GameLevel Model { get; }
    }
}
