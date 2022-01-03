using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using GameUnity.Assets.Scripts.Unity.Views.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class PlacementView : ViewMonoBehaviour, IPlacementView
    {
        [SerializeField] PrototypeLink _gridPiece;
        [SerializeField] PrototypeLink _construction;
        [SerializeField] PrototypeLink _ghost;

        private Action<System.Numerics.Vector2> _click;
        private GameInputs _inputs = new GameInputs();

        public DisposableViewKeeper<IGhostConstructionView> Ghost { get; private set; }
        public DisposableViewListKeeper<ICellView> Cells { get; private set; }
        public DisposableViewListKeeper<IConstructionView> Constructions { get; private set; }

        protected void Awake()
        {
            Ghost = new DisposableViewKeeper<IGhostConstructionView>(CreateGhost);
            Cells = new DisposableViewListKeeper<ICellView>(CreateCell);
            Constructions = new DisposableViewListKeeper<IConstructionView>(CreateConstrcution);
        }

        public ICellView CreateCell()
        {
            return _gridPiece.Create<GridPieceView>();
        }

        public IConstructionView CreateConstrcution()
        {
            return _construction.Create<BuildingView>();
        }

        public IGhostConstructionView CreateGhost()
        {
            return _ghost.Create<BuildingGhostView>();
        }

        public void Click(System.Numerics.Vector2 vector2)
        {
            _click(vector2);
        }

        public void SetClick(Action<System.Numerics.Vector2> onClick)
        {
            _click = onClick;
        }

        public void Update()
        {
            if (_inputs.IsTapedOnLevel())
                _click(_inputs.GetMouseWorldPosition());
        }
    }
}