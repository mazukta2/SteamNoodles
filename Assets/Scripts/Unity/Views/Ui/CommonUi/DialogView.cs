﻿using UnityEngine;
using System.Collections;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;

public class DialogView : UnitySimpleView, IDialogView
{
    [SerializeField] ButtonUnityView _button;
    [SerializeField] AnimatorUnity _animator;

    public IAnimator Animator => _animator;
    public IButton Next => _button;
}

