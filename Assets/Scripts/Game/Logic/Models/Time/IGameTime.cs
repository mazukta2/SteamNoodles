using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Time
{
    public interface IGameTime
    {
        event Action<float, float> OnTimeChanged;
        float Time { get; }
        static IGameTime Default { get; set; }
    }
}
