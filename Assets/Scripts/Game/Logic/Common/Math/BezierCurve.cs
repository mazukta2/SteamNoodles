using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Common.Math
{
    public class BezierCurve
    {
        // based on https://www.habrador.com/tutorials/interpolation/2-bezier-curve/

        public GameVector3 Start { get; private set; }
        public GameVector3 StartControl { get; private set; }
        public GameVector3 EndControl { get; private set; }
        public GameVector3 End { get; private set; }

        public BezierCurve(GameVector3 start, GameVector3 end, GameVector3 controlStart, GameVector3 controlEnd)
        {
            Start = start;
            End = end;
            StartControl = controlStart;
            EndControl = controlEnd;
        }

        public GameVector3 GetPosition(float time)
        {
            if (time < 0 || time > 1)
                throw new ArgumentException(nameof(time));

            return DeCasteljausAlgorithm(time);
        }

        //Display without having to press play
        public IReadOnlyCollection<GameVector3> GetLine()
        {
            var line = new List<GameVector3>();
            
            //The start position of the line
            GameVector3 lastPos = Start;
            line.Add(Start);

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
                GameVector3 newPos = DeCasteljausAlgorithm(t);

                line.Add(newPos);
            }

            return line.AsReadOnly();
        }

        GameVector3 DeCasteljausAlgorithm(float t)
        {
            var Q0 = GameVector3.Lerp(Start, StartControl, t);
            var Q1 = GameVector3.Lerp(StartControl, EndControl, t);
            var Q2 = GameVector3.Lerp(EndControl, End, t);

            var R0 = GameVector3.Lerp(Q0, Q1, t);
            var R1 = GameVector3.Lerp(Q1, Q2, t);

            var B  = GameVector3.Lerp(R0, R1, t);

            return B;
        }


    }
}
