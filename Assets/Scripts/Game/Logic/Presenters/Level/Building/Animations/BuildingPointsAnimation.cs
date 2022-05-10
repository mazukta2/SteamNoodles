using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations
{
    public class BuildingPointsAnimation : Disposable
    {
        public event Action OnPieceReachDestination = delegate { };
        private int _pointsToSpawn;
        private PointPieceSpawnerPresenter _pointPieceSpawner;
        private readonly ConstructionsSettingsDefinition _constructionsSettingsDefinition;
        private readonly IGameTime _time;
        private float _remainTimeToProcess;
        private BezierCurve _bezierCurve;
        private Dictionary<IPieceView, float> _pieces = new Dictionary<IPieceView, float>();

        public BuildingPointsAnimation(BezierCurve bezierCurve, int points, PointPieceSpawnerPresenter pointPieceSpawner,
            ConstructionsSettingsDefinition constructionsSettingsDefinition, IGameTime time)
        {
            _bezierCurve = bezierCurve;
            _pointsToSpawn = points;
            _pointPieceSpawner = pointPieceSpawner ?? throw new ArgumentNullException(nameof(pointPieceSpawner));
            _constructionsSettingsDefinition = constructionsSettingsDefinition ?? throw new ArgumentNullException(nameof(constructionsSettingsDefinition));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _time.OnTimeChanged += _time_OnTimeChanged;
            _pointPieceSpawner.OnDispose += Dispose;
        }

        protected override void DisposeInner()
        {
            _pointPieceSpawner.OnDispose -= Dispose;
            _time.OnTimeChanged -= _time_OnTimeChanged;

            foreach (var item in _pieces.ToList())
                item.Key.Dispose();
        }

        public void Play()
        {
            if (IsDisposed)
                throw new Exception("Cant play animation");


            if (_pointsToSpawn <= 0 || _constructionsSettingsDefinition.PieceMovingTime == 0)
            {
                for (int i = 0; i < _pointsToSpawn; i++)
                    OnPieceReachDestination();
                Dispose();
            }
            else
                SpawnPiece(_time.Time);
        }

        private void _time_OnTimeChanged(float oldTime, float newTime)
        {
            var timeToProcess = _remainTimeToProcess + newTime - oldTime;
            var timeForEveryPiece = _constructionsSettingsDefinition.PieceSpawningTime / _pointsToSpawn;

            while (_pointsToSpawn > 0 && timeToProcess >= timeForEveryPiece)
            {
                timeToProcess -= timeForEveryPiece;
                SpawnPiece(newTime - timeToProcess);
            }
            _remainTimeToProcess = timeToProcess;

            foreach (var item in _pieces.ToList())
            {
                var timePassedFromSpawn = newTime - item.Value;
                SetPosition(item.Key, timePassedFromSpawn);
                if (timePassedFromSpawn >= _constructionsSettingsDefinition.PieceMovingTime)
                {
                    item.Key.Dispose();
                    OnPieceReachDestination();
                }
            }

            if (_pieces.Count == 0)
                Dispose();
        }

        private void SpawnPiece(float currentTime)
        {
            var piece = _pointPieceSpawner.Spawn();
            piece.OnDispose += Piece_OnDispose;
            _pieces.Add(piece, currentTime);
            SetPosition(piece, 0);
            _pointsToSpawn--;

            void Piece_OnDispose()
            {
                piece.OnDispose -= Piece_OnDispose;
                _pieces.Remove(piece);
            }
        }

        private void SetPosition(IPieceView key, float timePassedFromSpawn)
        {
            var process = Math.Min(1, timePassedFromSpawn / _constructionsSettingsDefinition.PieceMovingTime);
            key.Position.Value = _bezierCurve.GetPosition(process);
        }

    }
}
