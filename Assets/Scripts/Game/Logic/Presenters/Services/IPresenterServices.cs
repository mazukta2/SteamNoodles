﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services
{
    public interface IPresenterServices
    {
        public static IPresenterServices Default { get; set; }
        public T Get<T>();
    }
}