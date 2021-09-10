using Assets.Scripts.Logic.Models.Events.GameEvents;
using Assets.Scripts.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class LevelViewModel
    {
        public LevelScreenViewModel Screen { get; private set; }
        public PlacementViewModel Placement { get; private set; }

        private GameLevel _model;

        public LevelViewModel(GameLevel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;

            Placement = new PlacementViewModel(model.Placement);
            Screen = new LevelScreenViewModel(model, Placement);
        }

    }
}
