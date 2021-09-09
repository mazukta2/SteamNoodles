using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Logic.Common.Math
{
    public interface IRect
    {
        int xMin { get; }
        int xMax { get; }
        int yMin { get; }
        int yMax { get; }

        bool IsInside(IPoint point);
    }
}
