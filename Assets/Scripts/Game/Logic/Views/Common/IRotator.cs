using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Views.Common
{
    public interface IRotator
    {
        FloatPoint3D GetDirection();
        void LookAtDirection(FloatPoint3D direction);
    }
}
