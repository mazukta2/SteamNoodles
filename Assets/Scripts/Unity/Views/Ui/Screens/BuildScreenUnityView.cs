﻿using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenPresenter>, IBuildScreenView
    {
        [SerializeField] ButtonUnityView _closeButton;
        [SerializeField] UnityWorldText _additionalPoints;
        [SerializeField] TextMeshProUGUI _currentPoints;
        [SerializeField] Image _progress;
        [SerializeField] Image _additionalProgress;

        public IButton CancelButton => _closeButton;
        public IWorldText Points => _additionalPoints;
        public IText CurrentPoints { get; private set; }
        public IProgressBar PointsProgress { get; private set; }

        protected override void PreAwake()
        {
            CurrentPoints = new UnityText(_currentPoints);
            PointsProgress = new UnityProgressBar(_progress, _additionalProgress);
        }
    }

}
