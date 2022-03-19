﻿using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class GhostUnityView : UnityView<GhostView>
    {
        protected override GhostView CreateView()
        {
            return new GhostView(Level, new UnityLevelPosition(transform));
        }

    }

}
