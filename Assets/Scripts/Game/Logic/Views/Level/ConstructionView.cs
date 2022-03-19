using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class ConstructionView : View
    {
        public FloatPoint LocalPosition { get; set; }
        private ConstructionPresenter _presenter;

        public ConstructionView(ILevel level) : base(level)
        {
        }

        public void Init(Construction construction)
        {
            _presenter = new ConstructionPresenter(construction, this);
        }
    }
}
