using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingScrollView : ViewMonoBehaviour, IHandView
    {
        [SerializeField] PrototypeLink _buildingButton;

        public DisposableViewListKeeper<IHandConstructionView> Cards => throw new System.NotImplementedException();

        public IHandConstructionView CreateConstruction()
        {
            return _buildingButton.Create<BuildingSchemeView>();
        }
    }
}