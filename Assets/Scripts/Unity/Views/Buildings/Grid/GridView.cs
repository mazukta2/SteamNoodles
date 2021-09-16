using Assets.Scripts.Core;
using Assets.Scripts.Core.Prototypes;
using GameUnity.Assets.Scripts.Unity.Views.Buildings;
using System;
using Tests.Assets.Scripts.Game.Logic.Views;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings.Grid
{
    public class GridView : GameMonoBehaviour, IPlacementView
    {
        [SerializeField] PrototypeLink _gridPiece;
        [SerializeField] PrototypeLink _construction;
        [SerializeField] PrototypeLink _ghost;

        private Action<System.Numerics.Vector2> _click;

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
    }
}