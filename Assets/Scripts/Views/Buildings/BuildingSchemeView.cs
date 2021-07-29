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
        [SerializeField] BuildingGhostView _ghostView;

        private BuildingScheme _buildingScheme;

        public void Set(BuildingScheme buildingScheme)
        {
            _buildingScheme = buildingScheme;
            _image.sprite = _buildingScheme.GetImage();
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
            if (_ghostView.Scheme == _buildingScheme)
            {
                _ghostView.ClearGhost();
            } 
            else if (_ghostView.Scheme != null)
            {
                _ghostView.ClearGhost();
                _ghostView.SetGhost(_buildingScheme);
            }
            else
            {
                _ghostView.SetGhost(_buildingScheme);
            }
        }
    }
}