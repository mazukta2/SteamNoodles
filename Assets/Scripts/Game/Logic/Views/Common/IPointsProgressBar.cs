using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IPointsProgressBar
    {
        float MainValue { get; set; }
        float AddedValue { get; set; }
        float RemovedValue { get; set; }
    }
}
