using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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
