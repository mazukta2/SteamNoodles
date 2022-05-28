using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Definitions
{
    public interface IDefinition
    {
        DefId DefId { get; set; }
    }
}
