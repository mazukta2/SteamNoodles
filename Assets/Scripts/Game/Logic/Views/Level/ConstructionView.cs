using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class ConstructionView : View
    {
        public ILevelPosition LocalPosition { get; set; }
        public IViewContainer Container { get; set; }

        private ConstructionPresenter _presenter;

        public ConstructionView(ILevel level, IViewContainer container, ILevelPosition position) : base(level)
        {
            LocalPosition = position;
            Container = container;
        }

        public void Init(Construction construction)
        {
            _presenter = new ConstructionPresenter(construction, Level.Engine.Assets, this);
        }
    }
}
