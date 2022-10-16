using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Environment.Engine
{
    public class UnityEnviroment : Disposable
    {
        public IAssets Assets { get; }
        public ILevelsManager Levels { get; }
        public IDefinitions Definitions { get; }
        public IControls Controls { get; }
        public IGameTime Time { get; }

        public UnityEnviroment(ILevelsManager levelsManager, IAssets assets, IDefinitions definitions, IControls controls, IGameTime time)
        {
            Levels = levelsManager;
            Assets = assets;
            Definitions = definitions;
            Controls = controls;
            Time = time;
        }
    }
}

