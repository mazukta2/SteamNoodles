using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ImageUnityView : MonoBehaviour, IImage
    {
        private Image _image;
        public string Path { get; private set; }

        private Image GetImage()
        {
            if (_image == null)
                _image = GetComponent<Image>();
            return _image;
        }

        protected void Awake()
        {
            UpdateImage();
        }


        private void UpdateImage()
        {
            if (!string.IsNullOrEmpty(Path))
            {
                var sprite = Resources.Load<Sprite>("Assets/"+Path);
                if (sprite == null)
                    throw new Exception($"Cant find {Path}");

                GetImage().sprite = sprite;
            }
        }

        public void SetPath(string path)
        {
            Path = path;
            UpdateImage();
        }
    }
}
