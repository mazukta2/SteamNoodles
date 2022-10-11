﻿using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using Game.Assets.Scripts.Game.Unity.Views.Ui;
using UnityEngine;

public class GameExitButtonUnityView : UnityView<ExitButtonPresenter>, IExitGameButtonView
{
    [SerializeField] ButtonUnityView _button;

    public IButton Button => _button;
}
