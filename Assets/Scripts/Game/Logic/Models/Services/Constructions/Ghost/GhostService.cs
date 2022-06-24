﻿using System;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost
{
    public class GhostService : IService
    {
        private ConstructionGhost _ghost;
        private readonly Field _field;

        public event Action OnShowed = delegate { };
        public event Action OnHided = delegate { };
        public event Action OnChanged = delegate { };

        public GhostService(Field field)
        {
            _field = field ?? throw new ArgumentNullException(nameof(field));
        }
        
        public void Show(ConstructionCard constructionCard)
        {
            _ghost = new ConstructionGhost(constructionCard, new FieldPosition(_field, 0,0), GameVector3.Zero, FieldRotation.Default);
            
            OnShowed();
        }

        public void Hide()
        {
            _ghost = null;
            OnHided();
        }

        public void Set(ConstructionGhost ghost)
        {
            if (!IsEnabled())
                throw new Exception("Not enabled");
            
            _ghost = ghost;
            OnChanged();
        }

        public bool IsEnabled() => GetGhost() != null;

        public ConstructionGhost GetGhost()
        {
            return _ghost;
        }

    }
}
