using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public class GhostBuildingService : Disposable, IService
    {
        private readonly GameControlsService _controlsService;
        private readonly GhostRepository _ghostRepository;

        public GhostBuildingService(
            GhostRepository ghostRepository,
            GameControlsService controlsService)
        {
            _ghostRepository = ghostRepository ?? throw new ArgumentNullException(nameof(ghostRepository));
            _controlsService = controlsService ?? throw new ArgumentNullException(nameof(controlsService));
            _controlsService.OnLevelClick += HandleOnLevelClick;
        }

        protected override void DisposeInner()
        {
            _controlsService.OnLevelClick -= HandleOnLevelClick;
        }

        private void HandleOnLevelClick()
        {
            // if (!_ghostRepository.Has())
            //     return;
            //
            // var ghost = _ghostRepository.Get();
            // ghost.TryBuild();
            // ghost.Destroy();
        }
    }
}
