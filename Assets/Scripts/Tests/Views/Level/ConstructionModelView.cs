using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Level
{
    public class ConstructionModelView : View, IConstructionModelView
    {
        public IAnimator Animator { get; } = new AnimatorMock();

        public IAnimator BorderAnimator { get; } = new AnimatorMock();

        public IFloat Shrink { get; } = new FloatMock();
        private List<IPresenter> _presenters = new List<IPresenter>();

        public ConstructionModelView(IViewsCollection level) : base(level)
        {
        }

        public void AddPresenter(IPresenter presenter)
        {
            _presenters.Add(presenter);
        }
    }
}
