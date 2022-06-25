using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Customers
{
    public class CustomerDefinition : IDefinition
    {
        public DefId DefId { get; set; }

        public void Validate()
        {
        }
    }
}
