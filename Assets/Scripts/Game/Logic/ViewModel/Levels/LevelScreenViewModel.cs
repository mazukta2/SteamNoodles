using Assets.Scripts.Logic;
using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel
{
    public class LevelScreenViewModel
    {
        public HandViewModel Hand { get; }

        private GameLevel _model;
        public LevelScreenViewModel(GameLevel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
            Hand = new HandViewModel(model.Hand);
        }
    }
}
