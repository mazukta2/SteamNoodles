using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PointAttractionPositionUnityView : UnitySimpleView, IPointAttractionPositionView
    {
        [SerializeField] UnityPosition _pointsAttractionPoint;
        [SerializeField] UnityPosition _pointsAttractionControlPoint;

        public IPosition PointsAttractionPoint => _pointsAttractionPoint;
        public IPosition PointsAttractionControlPoint => _pointsAttractionControlPoint;

        void OnDrawGizmos()
        {
            var beizer = new BezierCurve(new FloatPoint3D(0, 0, 0), _pointsAttractionPoint.Value, new FloatPoint3D(0, 4, 0), _pointsAttractionControlPoint.Value);

            //The Bezier curve's color
            Gizmos.color = Color.white;

            //The start position of the line
            Vector3 lastPos = beizer.Start.ToVector();

            //The resolution of the line
            //Make sure the resolution is adding up to 1, so 0.3 will give a gap at the end, but 0.2 will work
            float resolution = 0.02f;

            //How many loops?
            int loops = Mathf.FloorToInt(1f / resolution);

            for (int i = 1; i <= loops; i++)
            {
                //Which t position are we at?
                float t = i * resolution;

                //Find the coordinates between the control points with a Catmull-Rom spline
                Vector3 newPos = beizer.GetPosition(t).ToVector();

                //Draw this line segment
                Gizmos.DrawLine(lastPos, newPos);

                //Save this pos so we can draw the next line segment
                lastPos = newPos;
            }

            //Also draw lines between the control points and endpoints
            Gizmos.color = Color.green;

            Gizmos.DrawLine(beizer.Start.ToVector(), beizer.StartControl.ToVector());
            Gizmos.DrawLine(beizer.End.ToVector(), beizer.EndControl.ToVector());
        }
    }

}
