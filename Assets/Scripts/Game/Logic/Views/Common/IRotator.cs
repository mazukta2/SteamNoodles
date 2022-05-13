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
        void FaceTo(FloatPoint3D value);
        void Look(FloatPoint3D direction);
    }
}
