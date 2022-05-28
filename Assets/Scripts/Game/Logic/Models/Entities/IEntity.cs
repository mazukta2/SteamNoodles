using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities
{
    public interface IEntity
    {
        Uid Id { get; }

        IEntity Copy();
    }
}
