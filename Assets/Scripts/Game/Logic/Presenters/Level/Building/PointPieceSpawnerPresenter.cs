using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building
{
    public class PointPieceSpawnerPresenter : BasePresenter<IPointPieceSpawner>
    {
        private IPointPieceSpawner _view;
        private List<IPieceView> _pieces = new List<IPieceView>();

        public PointPieceSpawnerPresenter(IPointPieceSpawner view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            foreach (var piece in _pieces.ToArray())
                piece.Dispose();
            _pieces.Clear();
        }

        public IPieceView Spawn()
        {
            if (IsDisposed)
                throw new ObjectDisposedException(nameof(PointPieceSpawnerPresenter));

            var piece = _view.Container.Spawn<IPieceView>(_view.PiecePrefab);
            _pieces.Add(piece);
            piece.OnDispose += Piece_OnDispose;
            return piece;

            void Piece_OnDispose()
            {
                piece.OnDispose -= Piece_OnDispose;
                _pieces.Remove(piece);
            }
        }

    }
}
