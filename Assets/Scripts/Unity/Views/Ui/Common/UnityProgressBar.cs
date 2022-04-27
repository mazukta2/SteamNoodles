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
        private Image _negativeProgress;

        private float _value;
        private float _additional;

        public UnityProgressBar(Image progress, Image additional, Image negative)
        {
            _progress = progress;
            _additionalProgress = additional;
            _negativeProgress = negative;
            _progress.fillAmount = 0;
            _additionalProgress.fillAmount = 0;
        }

        public float Value { get => _value; set { _value = value; UpdateView();} }
        public float AdditonalValue { get => _additional; set { _additional = value; UpdateView(); } }

        private void UpdateView()
        {
            if (_additional < 0)
            {
                _progress.fillAmount = _value + _additional;
                _negativeProgress.fillAmount = _value;
                _additionalProgress.fillAmount = 0;
            }
            else
            {
                _progress.fillAmount = _value;
                _additionalProgress.fillAmount = _additional;
                _negativeProgress.fillAmount = 0;
            }


        }
    }
}
