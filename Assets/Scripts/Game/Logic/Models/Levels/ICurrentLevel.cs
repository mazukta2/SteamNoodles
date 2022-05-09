using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels
{
    public interface ICurrentLevel
    {
        public static GameLevel Default { get; set; }
    }
}
