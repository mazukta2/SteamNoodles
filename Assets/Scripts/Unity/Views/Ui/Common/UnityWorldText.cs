using System;
using Assets.Scripts.Views.Cameras;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using TMPro;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Common
{
    [Serializable]
    public class UnityWorldText : IWorldText
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Vector2 _screenOffset;

        private FloatPoint3D _position;

        public UnityWorldText()
        {

        }

        public string Value { get => _text.text; set => Set(value); }
        public FloatPoint3D Position { get => _position;
            set {
                _position = value;
                UpdateView();
            }
        }


        public void Set(string text)
        {
            _text.text = text;
        }


        private void UpdateView()
        {
            
            var pos = MainCameraController.Instance.WorldToUISpace(_text.canvas, _position.ToVector());
            _text.transform.position = pos + (Vector3)_screenOffset;
        }

    }
}
