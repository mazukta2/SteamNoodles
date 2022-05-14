﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class ConstructionUnityView : UnityView<ConstructionPresenter>, IConstructionView
    {
        [SerializeField] ContainerUnityView _container;
        [SerializeField] UnityPosition _position;

        public IPosition Position => _position;
        public IRotator Rotator { get; private set; }
        public IViewContainer Container => _container;

        protected override void PreAwake()
        {
            Rotator = new UnityRotator(transform);
        }
    }

}
