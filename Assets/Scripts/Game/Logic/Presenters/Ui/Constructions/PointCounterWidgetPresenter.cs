using Game.Assets.Scripts.Game.External;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class PointCounterWidgetPresenter : BasePresenter<IPointCounterWidgetView>
    {
        private readonly IPointCounterWidgetView _view;
        private Resources _resources;
        private GhostManagerPresenter _ghostManager;
        private PlacementField _placementField;
        private readonly IGameTime _time;
        private List<BuildingPointsAnimation> _animations = new List<BuildingPointsAnimation>();
        private BuildingPoints _localBuildingPoints;
        private ProgressBarSliders _progressBar;
        private int _pointChanges;

        public PointCounterWidgetPresenter(Resources resources, GhostManagerPresenter ghostManager, 
            PlacementField placementField, IGameTime time, IPointCounterWidgetView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _ghostManager = ghostManager ?? throw new ArgumentNullException(nameof(ghostManager));
            _placementField = placementField ?? throw new ArgumentNullException(nameof(placementField));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _localBuildingPoints = _resources.Points.Copy();
            _localBuildingPoints.OnLevelUp += HandleLevelChanged;
            _localBuildingPoints.OnLevelDown += HandleLevelChanged;

            var def = IDefinitions.Default.Get<ConstructionsSettingsDefinition>();
            _progressBar = new ProgressBarSliders(_view.PointsProgress, _time, def.PointsSliderFrequency, def.PointsSliderSpeed);

            _ghostManager.OnGhostChanged += HandleGhostUpdate;
            _placementField.OnPointChangedDuringConstruction += HandleOnPointsChangedDuringConstruction;
            _ghostManager.OnGhostPostionChanged += HandleGhostUpdate;
            _view.Animator.OnFinished += HandleAnimationFinished;

            _view.PointsProgress.RemovedValue = 0;
            _view.PointsProgress.AddedValue = 0;
            _view.PointsProgress.MainValue = 0;

            UpdateValues();
        }

        protected override void DisposeInner()
        {
            _progressBar.Dispose();

            _placementField.OnPointChangedDuringConstruction -= HandleOnPointsChangedDuringConstruction;
            _ghostManager.OnGhostChanged -= HandleGhostUpdate;
            _ghostManager.OnGhostPostionChanged -= HandleGhostUpdate;
            _localBuildingPoints.OnLevelUp -= HandleLevelChanged;
            _localBuildingPoints.OnLevelDown -= HandleLevelChanged;
            _view.Animator.OnFinished -= HandleAnimationFinished;
        }

        private void HandleOnPointsChangedDuringConstruction(Construction construction, int pointsAdded)
        {
            var curve = new BezierCurve(construction.GetWorldPosition(),
                IPointCounterWidgetView.Default.PointsAttractionPoint.Value,
                construction.GetWorldPosition() + new GameVector3(0, 4, 0),
                IPointCounterWidgetView.Default.PointsAttractionControlPoint.Value);

            var animation = new BuildingPointsAnimation(curve, pointsAdded,
                IPointPieceSpawnerView.Default.Presenter,
                IDefinitions.Default.Get<ConstructionsSettingsDefinition>(), _time);
            animation.OnPieceReachDestination += OnPieceReachDestination;
            animation.OnDispose += Animation_OnDispose;
            _animations.Add(animation);

            if (_animations.Count == 1)
                animation.Play();

            void Animation_OnDispose()
            {
                animation.OnPieceReachDestination -= OnPieceReachDestination;
                animation.OnDispose -= Animation_OnDispose;
                _animations.Remove(animation);

                _localBuildingPoints.Value = _resources.Points.Value;

                if (_animations.Count > 0)
                    _animations.First().Play();
                
            }
        }

        private void OnPieceReachDestination()
        {
            _view.Animator.Play(Animations.Change.ToString(), true);
            _localBuildingPoints.Value += 1;
            UpdateValues();
        }

        private void HandleGhostUpdate()
        {
            var ghost = _ghostManager.GetGhost();
            if (ghost != null)
                SetPotentialPoints(ghost.GetPointChanges());
            else
                SetPotentialPoints(0);
        }

        public void SetPotentialPoints(int pointChanges)
        {
            _pointChanges = pointChanges;
            UpdateValues();
        }

        private void HandleLevelChanged()
        {
            _view.Animator.Play(Animations.Level.ToString(), true);
            _progressBar.Skip();
        }

        private void UpdateValues()
        {
            _view.Points.Value = $"{_localBuildingPoints.Value}/{_localBuildingPoints.PointsForNextLevel}";
            _progressBar.Value = _localBuildingPoints.Progress;

            if (_pointChanges != 0)
            {
                var newPoints = _localBuildingPoints.GetChangedValue(_pointChanges);
                if (newPoints.Value < _localBuildingPoints.Value)
                {
                    _progressBar.Value = newPoints.Progress;
                    if (newPoints.CurrentLevel == _localBuildingPoints.CurrentLevel)
                        _progressBar.Remove = _localBuildingPoints.Progress;
                    else
                        _progressBar.Remove = 1;
                    _progressBar.Add = 0;
                }
                else
                {
                    _progressBar.Value = _localBuildingPoints.Progress;
                    if (newPoints.CurrentLevel == _localBuildingPoints.CurrentLevel)
                        _progressBar.Add = newPoints.Progress;
                    else
                        _progressBar.Add = 1;
                    _progressBar.Remove = 0;
                }
            }
            else
            {
                _progressBar.Add = 0;
                _progressBar.Remove = 0;
            }

        }

        private void HandleAnimationFinished()
        {
            _view.Animator.Play(Animations.Idle.ToString());
        }

        enum Animations
        {
            Idle,
            Change,
            Level
        }
    }
}
