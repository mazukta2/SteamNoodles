using Assets.Scripts.Core;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingSchemeView : ViewMonoBehaviour, IHandConstructionView
    {
        [SerializeField] Image _image;
        [SerializeField] Button _button;
        private Action _click;

        protected void OnEnable()
        {
            _button.onClick.AddListener(Click);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(Click);
        }

        public void SetIcon(ISprite icon)
        {
            _image.sprite = ((UnitySprite)icon).Sprite;
        }

        public ISprite GetIcon()
        {
            return new UnitySprite(_image.sprite);
        }

        public void SetClick(Action action)
        {
            _click = action;
        }

        public void Click()
        {
            _click();
        }
    }
}