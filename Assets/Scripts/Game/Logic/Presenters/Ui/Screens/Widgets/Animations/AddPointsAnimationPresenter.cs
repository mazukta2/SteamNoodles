using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using static Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points.BuildingPointsAnimations.AddPointsAnimation;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations
{
    public class AddPointsAnimationPresenter : Disposable
    {
        private PointPieceSpawnerPresenter _pointPieceSpawner;
        private readonly AddPointsAnimation _animation;
        private BezierCurve _bezierCurve;
        private Dictionary<PieceModel, IPieceView> _pieces = new Dictionary<PieceModel, IPieceView>();

        public AddPointsAnimationPresenter(BezierCurve bezierCurve, PointPieceSpawnerPresenter pointPieceSpawner, AddPointsAnimation animation)
        {
            _bezierCurve = bezierCurve;
            _pointPieceSpawner = pointPieceSpawner ?? throw new ArgumentNullException(nameof(pointPieceSpawner));
            _animation = animation;
            _animation.OnPiecesSpawned += HandleOnPiecesSpawned;
            _pointPieceSpawner.OnDispose += Dispose;
            _animation.OnDispose += Dispose;
        }

        protected override void DisposeInner()
        {
            _animation.OnDispose -= Dispose;
            _pointPieceSpawner.OnDispose -= Dispose;

            foreach (var item in _pieces.ToList())
                item.Value.Dispose();
        }

        private void HandleOnPiecesSpawned(PieceModel obj)
        {
            var piece = _pointPieceSpawner.Spawn();
            _pieces.Add(obj, piece);
            SetPosition(obj);
            piece.OnDispose += Piece_OnDispose;
            obj.OnDispose += Obj_OnDispose;
            obj.OnProcessChanged += Obj_OnProcessChanged;

            void Piece_OnDispose()
            {
                piece.OnDispose -= Piece_OnDispose;
                obj.OnDispose -= Obj_OnDispose;
                obj.OnProcessChanged -= Obj_OnProcessChanged;
            }

            void Obj_OnDispose()
            {
                _pieces[obj].Dispose();
                _pieces.Remove(obj);
            }

            void Obj_OnProcessChanged()
            {
                SetPosition(obj);
            }
        }

        private void SetPosition(PieceModel key)
        {
            _pieces[key].Position.Value = _bezierCurve.GetPosition(key.Process);
        }

    }
}
