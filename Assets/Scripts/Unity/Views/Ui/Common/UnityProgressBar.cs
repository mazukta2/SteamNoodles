using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    public class UnityProgressBar : IProgressBar
    {
        private Image _progress;
        private Image _additionalProgress;

        public UnityProgressBar(Image progress, Image additional)
        {
            _progress = progress;
            _additionalProgress = additional;
            _progress.fillAmount = 0;
            _additionalProgress.fillAmount = 0;
        }

        public float Value { get => _progress.fillAmount; set => _progress.fillAmount = value; }
        public float AdditonalValue { get => _additionalProgress.fillAmount; set => _additionalProgress.fillAmount = value; }
    }
}
