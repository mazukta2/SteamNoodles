using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.BuildingPointsAnimations
{
    public class AddPointsAnimation : Disposable
    {
        public event Action OnPieceReachDestination = delegate { };
        public event Action<PieceModel> OnPiecesSpawned = delegate { };
        public GameVector3 Postion { get; }

        private int _pointsToSpawn;
        private readonly ConstructionsSettingsDefinition _constructionsSettingsDefinition;
        private readonly IGameTime _time;
        private float _remainTimeToProcess;
        private Dictionary<PieceModel, float> _pieces = new Dictionary<PieceModel, float>();

        public AddPointsAnimation(int points, ConstructionsSettingsDefinition constructionsSettingsDefinition, IGameTime time, Common.Math.GameVector3 postion)
        {
            _pointsToSpawn = points;
            _constructionsSettingsDefinition = constructionsSettingsDefinition ?? throw new ArgumentNullException(nameof(constructionsSettingsDefinition));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            Postion = postion;
            _time.OnTimeChanged += _time_OnTimeChanged;
        }

        protected override void DisposeInner()
        {
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
            var piece = new PieceModel();
            piece.OnDispose += Piece_OnDispose;
            _pieces.Add(piece, currentTime);
            SetPosition(piece, 0);
            _pointsToSpawn--;

            OnPiecesSpawned(piece);

            void Piece_OnDispose()
            {
                piece.OnDispose -= Piece_OnDispose;
                _pieces.Remove(piece);
            }
        }

        private void SetPosition(PieceModel key, float timePassedFromSpawn)
        {
            key.ChangeProcess(Math.Min(1, timePassedFromSpawn / _constructionsSettingsDefinition.PieceMovingTime));
        }

        public class PieceModel : Disposable
        {
            public event Action OnProcessChanged = delegate { };
            public float Process { get; private set; }

            public void ChangeProcess(float value)
            {
                Process = value;
                OnProcessChanged();
            }
        }
    }
}
