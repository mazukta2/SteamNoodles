using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public class BezierCurve
    {
        // based on https://www.habrador.com/tutorials/interpolation/2-bezier-curve/

        FloatPoint3D _start, _startControl, _endControl, _end;

        public BezierCurve(FloatPoint3D start, FloatPoint3D end, FloatPoint3D controlStart, FloatPoint3D controlEnd)
        {
            _start = start;
            _end = end;
            _startControl = controlStart;
            _endControl = controlEnd;
        }

        public FloatPoint3D GetPosition(float time)
        {
            if (time < 0 || time > 1)
                throw new ArgumentException(nameof(time));

            return DeCasteljausAlgorithm(time);
        }

        //Display without having to press play
        public IReadOnlyCollection<FloatPoint3D> GetLine()
        {
            var line = new List<FloatPoint3D>();
            
            //The start position of the line
            FloatPoint3D lastPos = _start;
            line.Add(_start);

            //The resolution of the line
            //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
            float resolution = 0.02f;

            //How many loops?
            int loops = (int)(1f / resolution);

            for (int i = 1; i <= loops; i++)
            {
                //Which t position are we at?
                float t = i * resolution;

                //Find the coordinates between the control points with a Catmull-Rom spline
                FloatPoint3D newPos = DeCasteljausAlgorithm(t);

                line.Add(newPos);
            }

            return line.AsReadOnly();
        }

        FloatPoint3D DeCasteljausAlgorithm(float t)
        {
            var Q0 = FloatPoint3D.Lerp(_start, _startControl, t);
            var Q1 = FloatPoint3D.Lerp(_startControl, _endControl, t);
            var Q2 = FloatPoint3D.Lerp(_endControl, _end, t);

            var R0 = FloatPoint3D.Lerp(Q0, Q1, t);
            var R1 = FloatPoint3D.Lerp(Q1, Q2, t);

            var B  = FloatPoint3D.Lerp(R0, R1, t);

            return B;
        }


    }
}
