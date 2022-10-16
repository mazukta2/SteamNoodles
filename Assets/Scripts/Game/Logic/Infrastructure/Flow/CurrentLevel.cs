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
        private ILevel _level;
        private IViews _views;


        public CurrentLevel(ILevelsManager levelsManager, ILevel level, IModels models, IViews views)
        {
            _levelManager = levelsManager;
            _level = level;
            _views = views;

            IModels.Default = models;
            IViews.Default = views;
        }

        protected override void DisposeInner()
        {
            IModels.Default.Dispose();
            IViews.Default.Dispose();
            IModels.Default = null;
            IViews.Default = null;

            _level.Dispose();
        }

        public IViews GetViews()
        {
            return _views;
        }

        public void Start()
        { 
            _level.Start();
        }
    }
}

