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
    public class UnityPointProgressBar : IPointsProgressBar
    {
        private Image _progress;
        private Image _additionalProgress;
        private Image _negativeProgress;

        public UnityPointProgressBar(Image progress, Image additional, Image negative)
        {
            _progress = progress;
            _additionalProgress = additional;
            _negativeProgress = negative;

            _progress.fillAmount = 0;
            _additionalProgress.fillAmount = 0;
            _negativeProgress.fillAmount = 0;
        }

        public float MainValue { get => _progress.fillAmount; set => _progress.fillAmount = value; }
        public float AddedValue { get => _additionalProgress.fillAmount; set => _additionalProgress.fillAmount = value; }
        public float RemovedValue { get => _negativeProgress.fillAmount; set => _negativeProgress.fillAmount = value; }

    }
}
