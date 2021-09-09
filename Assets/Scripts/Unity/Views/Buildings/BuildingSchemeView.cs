using Assets.Scripts.Core;
using Assets.Scripts.ViewModels.Buildings;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingSchemeView : GameMonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Button _button;

        //private BuildingSchemeViewModel _buildingScheme;
        //private Action<BuildingSchemeViewModel> _onClick;

        //public void Set(BuildingSchemeViewModel buildingScheme, Action<BuildingSchemeViewModel> onClick)
        //{
        //    _buildingScheme = buildingScheme;
        //    _image.sprite = _buildingScheme.BuildingIcon;
        //    _onClick = onClick;
        //}

        //protected void OnEnable()
        //{
        //    _button.onClick.AddListener(OnClick);
        //}

        //protected void OnDisable()
        //{
        //    _button.onClick.RemoveListener(OnClick);
        //}

        //private void OnClick()
        //{
        //    _onClick(_buildingScheme);
        //}
    }
}