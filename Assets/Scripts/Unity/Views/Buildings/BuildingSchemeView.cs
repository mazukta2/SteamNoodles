using Assets.Scripts.Core;
using GameUnity.Assets.Scripts.Unity.Common;
using System;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views.Common;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingSchemeView : GameMonoBehaviour, IHandConstructionView
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