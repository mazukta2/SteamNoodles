﻿using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Level
{
    public class ConstructionModelView : View, IConstructionModelView
    {
        public ConstructionModelView(LevelView level) : base(level)
        {
        }
    }
}