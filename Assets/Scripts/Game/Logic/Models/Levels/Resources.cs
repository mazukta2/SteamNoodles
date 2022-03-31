using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class Resources : Disposable
    {
        public BuildingPoints Points { get; } = new BuildingPoints();
    }
}
