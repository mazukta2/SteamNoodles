﻿using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class ConstructionModelUnityView : UnitySimpleView, IConstructionModelView
    {
        [SerializeField] AnimatorUnity _animator;
        public IAnimator Animator => _animator;
        [SerializeField] AnimatorUnity _borderAnimator;
        public IAnimator BorderAnimator => _borderAnimator;

        [SerializeField] UnityShrinker _shrinker;
        public IFloat Shrink => _shrinker;
    }

}
