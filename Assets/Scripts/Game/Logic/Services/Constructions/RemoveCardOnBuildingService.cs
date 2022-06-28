using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions
{
    public class RemoveCardOnBuildingService : Disposable, IService
    {
        private readonly IDatabase<ConstructionEntity> _constructions;
        private readonly HandService _handService;

        public RemoveCardOnBuildingService(IDatabase<ConstructionEntity> constructions, HandService handService)
        {
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            _handService = handService ?? throw new ArgumentNullException(nameof(handService));
            _constructions.OnEvent += HandleEvent;
        }

        protected override void DisposeInner()
        {
            _constructions.OnEvent -= HandleEvent;
        }

        private void HandleEvent(ConstructionEntity constructionEntity, IModelEvent e)
        {
            if (e is not ConstructionBuiltByPlayerEvent builtByPlayerEvent)
                return;
            
            if (!_handService.Has(builtByPlayerEvent.CardEntity))
                throw new Exception("No such card in hand");
            
            _handService.Remove(builtByPlayerEvent.CardEntity);
        }
    }
}
