﻿using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Models.Levels.Types
{
    public interface IBattleLevel : ILevel
    {
        new static IBattleLevel Default { get; set; }

        FlowManager Flow { get; }
        BuildingService Building { get; }
        SchemesService Schemes { get; }
        HandService Hand { get;}
        LevelUnits Units { get; }
        FieldService Field { get; }
        CustomerQueue Queue { get; }
        Resources Resources { get; }
    }
}
