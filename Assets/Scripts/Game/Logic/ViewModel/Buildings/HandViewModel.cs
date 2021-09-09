using Assets.Scripts.Logic.Models.Levels;
using Assets.Scripts.Models.Buildings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Assets.Scripts.Game.Logic.ViewModel.Levels
{
    public class HandViewModel
    {
        private PlayerHand _model;

        public HandViewModel(PlayerHand model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            _model = model;
        }
    }
}
