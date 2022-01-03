using Assets.Scripts.Core;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class NumberView : ViewMonoBehaviour, IIntValueView
    {
        [SerializeField] TextMeshProUGUI _text;

        private int _value;

        public int GetValue()
        {
            return _value;
        }

        public void SetValue(int value)
        {
            _value = value;
            _text.text = _value.ToString();
        }
    }
}
