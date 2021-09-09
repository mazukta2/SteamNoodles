using Assets.Scripts.Logic.Models.Levels;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Common;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class LevelViewModel
    {
        public AutoUpdatedProperty<LevelScreenViewModel> Level { get; private set; }
        private GameLevel _model;

        public LevelViewModel(GameLevel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
        }
    }
}
