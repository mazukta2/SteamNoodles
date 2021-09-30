using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Actions.Game
{
    public class PlaceConstructionAction : GameAction
    {
        public ConstructionScheme Scheme { get; }
        public Point Position { get; }

        public PlaceConstructionAction(ConstructionScheme scheme, Point position)
        {
            Scheme = scheme;
            Position = position;
        }

        public override void Execute()
        {

        }
    }
}
