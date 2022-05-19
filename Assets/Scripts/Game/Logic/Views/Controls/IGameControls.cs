using Game.Assets.Scripts.Game.Environment.Engine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Controls
{
    public interface IGameControls : IControls, IDisposable
    {
        static IGameControls Default { get; set; }
    }
}
