using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;

namespace Game.Assets.Scripts.Game.Logic.Infrastructure.Flow
{
    public class CurrentLevel : Disposable
    {
        private ILevelsManager _levelManager;
        private ILevel _model;
        private IViews _views;


        public CurrentLevel(ILevelsManager levelsManager, ILevel level, IModels models, IViews views)
        {
            _levelManager = levelsManager;
            _model = level;
            _views = views;

            IModels.Default = models;
            IViews.Default = views;

            _model.Start();
        }

        protected override void DisposeInner()
        {
            IModels.Default = null;
            IViews.Default = null;

            _views.Dispose();
            _model.Dispose();
        }

        public IViews GetViews()
        {
            return _views;
        }
    }
}

