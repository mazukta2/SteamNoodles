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

        public UnityProgressBar(Image progress)
        {
            _progress = progress;
        }

        public float Value { get => _progress.fillAmount; set => _progress.fillAmount = value; }
    }
}
