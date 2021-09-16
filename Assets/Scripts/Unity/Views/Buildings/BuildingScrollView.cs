using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Levels;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : GameMonoBehaviour, IHandView
    {
        [SerializeField] PrototypeLink _buildingButton;

        public IHandConstructionView CreateConstruction()
        {
            return _buildingButton.Create<BuildingSchemeView>();
        }
    }
}