﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public interface IFieldModel : IDisposable
    {
        event Action OnUpdate;
        event Action<Uid> OnConstructionBuilded;
        FieldBoundaries Boudaries { get; }
        GameVector3 GetCellWorldPosition(IntPoint position);
        CellPlacementStatus GetStatus(IntPoint position);
        ConstructionPresenter CreatePresenter(IConstructionView view, Uid id);
    }
}
