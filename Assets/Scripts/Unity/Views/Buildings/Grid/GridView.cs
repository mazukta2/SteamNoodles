using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Constructions;
using GameUnity.Assets.Scripts.Unity.Views.Buildings;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridView : ViewMonoBehaviour, IPlacementView
    {
        [SerializeField] PrototypeLink _gridPiece;
        [SerializeField] PrototypeLink _construction;
        [SerializeField] PrototypeLink _ghost;

        private Action<System.Numerics.Vector2> _click;
        private GameInputs _inputs = new GameInputs();

        public DisposableViewKeeper<IGhostConstructionView> Ghost => throw new NotImplementedException();

        public DisposableViewListKeeper<ICellView> Cells => throw new NotImplementedException();

        public DisposableViewListKeeper<IConstructionView> Constructions => throw new NotImplementedException();

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