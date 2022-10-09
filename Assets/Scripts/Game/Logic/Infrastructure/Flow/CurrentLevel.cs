using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Flow
{
    public class CurrentLevel : Disposable
    {
        private ILevelsManager _levelManager;
        private ILevel _model;
        private IViewsCollection _views;


        public CurrentLevel(ILevelsManager levelsManager, ILevel level, IViewsCollection views)
        {
            _levelManager = levelsManager;
            _model = level;
            _views = views;

            _model.Start();
        }

        protected override void DisposeInner()
        {
            _views.Dispose();
            _model.Dispose();
        }

        public IViewsCollection GetViews()
        {
            return _views;
        }
    }
}

