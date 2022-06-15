using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services;
using Game.Assets.Scripts.Game.Logic.Models.Services.Session;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Screens;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Levels
{
    public class StageLevelPresenterService : Disposable, IService
    {
        private readonly IModelServices _services;

        public StageLevelPresenterService(StageLevel level)
            : this(level, IModelServices.Default,IGameRandom.Default, IGameTime.Default)
        {

        }

        public StageLevelPresenterService(StageLevel level, IModelServices services, IGameRandom random, IGameTime time)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _services.Add(new ScreenService());
            _services.Add(new GhostService());
        }

        protected override void DisposeInner()
        {
            _services.Remove<ScreenService>();
            _services.Remove<GhostService>();
        }

    }
}
