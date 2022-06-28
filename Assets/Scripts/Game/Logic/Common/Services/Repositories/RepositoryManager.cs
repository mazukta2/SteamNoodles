using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Databases;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public class RepositoryManager
    {
        private List<IBasicDatabase> _list = new List<IBasicDatabase>();

        public void Add(IBasicDatabase repository)
        {
            _list.Add(repository);
        }

        public void Remove(IBasicDatabase repository)
        {
            _list.Remove(repository);
        }
    }
}
