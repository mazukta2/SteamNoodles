using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions
{
    public interface IPointAttractionPositionView : IView, IViewWithDefaultPresenter
    {
        IPosition PointsAttractionPoint { get; }
        IPosition PointsAttractionControlPoint { get; }

        static IPointAttractionPositionView Default { get; set; }

        void IViewWithDefaultPresenter.Init()
        {
            Default = this;
        }
    }
}
