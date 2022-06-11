using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IGhostModel : IDisposable
    {
        event Action OnUpdate;
        bool IsActive { get; }
        GameVector3 WorldPosition();
        GameVector3 GetTargetPosition();
    }
}
