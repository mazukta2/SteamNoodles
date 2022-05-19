using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Assets
{
    public interface IGameAssets : IAssets
    {
        static IGameAssets Default { get; set; }
    }
}
