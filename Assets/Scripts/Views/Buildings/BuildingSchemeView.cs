using Assets.Scripts.Core;
using Assets.Scripts.Models.Buildings;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingSchemeView : GameMonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Button _button;

        private BuildingScheme _buildingScheme;
        private Action<BuildingScheme> _onClick;

        public void Set(BuildingScheme buildingScheme, Action<BuildingScheme> onClick)
        {
            _buildingScheme = buildingScheme;
            _image.sprite = _buildingScheme.GetImage();
            _onClick = onClick;
        }

        protected void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        protected void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            _onClick(_buildingScheme);
        }
    }
}