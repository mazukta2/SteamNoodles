using Game.Assets.Scripts.Game.Logic.Common.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public class Resources : Disposable
    {

        public event Action OnPointsChanged = delegate { };
        public int Points { get => _points; set
            { 
                _points = value;
                OnPointsChanged();
            }
        }
        private int _points;
    }
}
