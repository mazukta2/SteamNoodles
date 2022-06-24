using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities
{
    public interface IEntity
    {
        Uid Id { get; }

        IEntity Copy();
        IReadOnlyCollection<IModelEvent> GetEvents();
        void Clear();
    }
}
