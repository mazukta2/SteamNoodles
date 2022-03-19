using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui
{
    public class ImageUnityView : UnityView<ImageView>
    {
        private Image _image;
        private Image GetImage()
        {
            if (_image == null)
                _image = GetComponent<Image>();
            return _image;
        }

        protected override void AfterAwake()
        {
            UpdateImage();
            View.OnImageSetted += UpdateImage;
        }

        protected override void OnDisposeView(ImageView view)
        {
            view.OnImageSetted -= UpdateImage;
        }

        protected override ImageView CreateView()
        {
            return new ImageView(Level);
        }

        private void UpdateImage()
        {
            if (!string.IsNullOrEmpty(View.Path))
            {
                var sprite = Resources.Load<Sprite>("Assets/"+View.Path);
                if (sprite == null)
                    throw new Exception($"Cant find {View.Path}");

                GetImage().sprite = sprite;
            }
        }

    }
}
