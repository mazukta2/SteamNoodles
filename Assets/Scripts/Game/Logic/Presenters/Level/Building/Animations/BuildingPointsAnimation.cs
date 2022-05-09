using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations
{
    public class BuildingPointsAnimation : Disposable
    {
        private int SpawnTime => _constructionsSettingsDefinition.PieceSpawningTime.ToMs();
        private int MoveTime => _constructionsSettingsDefinition.PieceMovingTime.ToMs();

        private Construction _construction;
        private int _points;
        private Task _task;
        private PointPieceSpawnerPresenter _pointPieceSpawner;
        private readonly ConstructionsSettingsDefinition _constructionsSettingsDefinition;
        private CancellationTokenSource _tokenSource;

        public BuildingPointsAnimation(Construction construction, int points, PointPieceSpawnerPresenter pointPieceSpawner, ConstructionsSettingsDefinition constructionsSettingsDefinition)
        {
            _construction = construction ?? throw new ArgumentNullException(nameof(construction));
            _points = points;
            _pointPieceSpawner = pointPieceSpawner ?? throw new ArgumentNullException(nameof(pointPieceSpawner));
            _constructionsSettingsDefinition = constructionsSettingsDefinition ?? throw new ArgumentNullException(nameof(constructionsSettingsDefinition));
            _construction.OnDispose += Dispose;
            _pointPieceSpawner.OnDispose += Dispose;

            _tokenSource = new CancellationTokenSource();
        }

        protected override void DisposeInner()
        {
            _construction.OnDispose -= Dispose;
            _pointPieceSpawner.OnDispose -= Dispose;
            _tokenSource.Cancel();
        }

        public void Play()
        {
            if (_task != null || IsDisposed)
                throw new Exception("Cant play animation");

            _task = Task.Run(PlayAnimation, _tokenSource.Token);
        }

        public void PlayAnimation()
        {
            if (_tokenSource.IsCancellationRequested)
                return;

            var amount = _points;
            var timeForEveryPiece = SpawnTime / amount;

            var passedTime = 0;
            for (int i = 0; i < amount; i++)
            {
                SpawnPiece();
                Thread.Sleep(timeForEveryPiece);

                if (_tokenSource.IsCancellationRequested)
                    return;

                passedTime += timeForEveryPiece;
            }

            Thread.Sleep(MoveTime);

            if (_tokenSource.IsCancellationRequested)
                return;

            Dispose();
        }

        private void SpawnPiece()
        {
            _pointPieceSpawner.Spawn();
        }
    }
}
