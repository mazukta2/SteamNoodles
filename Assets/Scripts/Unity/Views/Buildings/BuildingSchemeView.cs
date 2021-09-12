using Assets.Scripts.Core;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingSchemeView : GameMonoBehaviour
    {
        [SerializeField] Image _image;
        [SerializeField] Button _button;
        private HandConstructionViewModel _buildingScheme;

        public void Set(HandConstructionViewModel buildingScheme)
        {
            _buildingScheme = buildingScheme;
            //_image.sprite = _buildingScheme.BuildingIcon;
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
            _buildingScheme.OnClick();
        }
    }
}