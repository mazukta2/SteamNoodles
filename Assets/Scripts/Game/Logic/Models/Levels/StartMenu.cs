using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Types;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class StartMenu : Disposable, ILevel
    {

        public StartMenu(LevelDefinition settings, IGameRandom random, IGameTime time, IGameDefinitions definitions)
        {
            Definition = settings;
        }


        public LevelDefinition Definition { get; set; }


        protected override void DisposeInner()
        {
        }
    }
}
