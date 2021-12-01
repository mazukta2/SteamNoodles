using Game.Assets.Scripts.Game.Logic.Views.Units;
using System;
using System.Collections.Generic;
using System.Text;
using Tests.Assets.Scripts.Game.Logic.Views;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels
{
    public interface IScreenView : IView
    {
        public IClashesView Clashes { get; }
        IClashesView CreateClashes();
        IHandView CreateHand();
        ICurrentOrderView CreateCurrentOrder();
    }
}
