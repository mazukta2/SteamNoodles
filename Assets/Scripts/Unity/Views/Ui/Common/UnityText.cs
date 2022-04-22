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

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityText : IText
    {
        [SerializeField] private TextMeshProUGUI _text;

        public UnityText()
        {

        }

        public UnityText(TextMeshProUGUI text)
        {
            _text = text;
        }

        public string Value { get => _text.text; set => Set(value); }
        public void Set(string text)
        {
            _text.text = text;
        }
    }
}
