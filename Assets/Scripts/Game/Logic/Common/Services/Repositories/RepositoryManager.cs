﻿using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Repositories;

namespace Game.Assets.Scripts.Game.Logic.Common.Services.Repositories
{
    public class RepositoryManager
    {
        private List<IBaseRepository> _list = new List<IBaseRepository>();

        public void Add(IBaseRepository repository)
        {
            _list.Add(repository);
        }

        public void Remove(IBaseRepository repository)
        {
            _list.Remove(repository);
        }
    }
}
