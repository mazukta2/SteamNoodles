using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class HandView : ViewMonoBehaviour, IHandView
    {
        [SerializeField] PrototypeLink _buildingButton;

        public DisposableViewListKeeper<IHandConstructionView> Cards { get; private set; }

        protected void Awake()
        {
            Cards = new DisposableViewListKeeper<IHandConstructionView>(CreateConstruction);
        }

        public IHandConstructionView CreateConstruction()
        {
            return _buildingButton.Create<BuildingSchemeView>();
        }
    }
}