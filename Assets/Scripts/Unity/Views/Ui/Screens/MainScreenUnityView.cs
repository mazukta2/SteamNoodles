﻿using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class MainScreenUnityView : ScreenUnityView<MainScreenPresenter>, IMainScreenView
    {
        [SerializeField] HandUnityView _hand;
        [SerializeField] TextMeshProUGUI _points;
        [SerializeField] Image _progress;
        [SerializeField] Image _additionalProgress;

        public IHandView HandView => _hand;
        public IText Points => new UnityText(_points);
        public IProgressBar PointsProgress => new UnityProgressBar(_progress, _additionalProgress);
    }
}
