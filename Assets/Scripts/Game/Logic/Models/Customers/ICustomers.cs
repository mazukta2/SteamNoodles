using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public interface ICustomers
    {
        FloatPoint3D GetQueueFirstPosition();
        int GetQueueSize();
    }
}
