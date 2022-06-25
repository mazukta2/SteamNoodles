using System;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Services.Constructions.Ghost
{
    public class GhostService : IService, IGhostCommands
    {
        private readonly ISingletonRepository<ConstructionGhost> _ghost;
        private readonly Field _field;

        public GhostService(ISingletonRepository<ConstructionGhost> ghost, Field field)
        {
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }
        
        public void Show(ConstructionCard constructionCard)
        {
            _ghost.Add(new ConstructionGhost(constructionCard, new FieldPosition(_field)));
        }

        public void Hide()
        {
            if (_ghost.Has())
                _ghost.Remove();
        }
    }
}
