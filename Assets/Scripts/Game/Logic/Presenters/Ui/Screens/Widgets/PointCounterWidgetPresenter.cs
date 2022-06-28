using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Customers;
using Game.Assets.Scripts.Game.Logic.Models.Customers.BuildingPointsAnimations;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets.Animations;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class PointCounterWidgetPresenter : BasePresenter<IPointCounterWidgetView>
    {
        private readonly IPointCounterWidgetView _view;
        private readonly IPointPieceSpawnerView _pointPieceSpawner;
        private BuildingPointsManager _points;
        private IGhostPresenter _ghostManager;
        private readonly IGameTime _time;
        private ProgressBarSliders _progressBar;
        private int _pointChanges;

        public PointCounterWidgetPresenter(BuildingPointsManager points, IGhostPresenter ghostManager,
            IGameTime time, IPointCounterWidgetView view, IPointPieceSpawnerView pointPieceSpawner,
            ConstructionsSettingsDefinition constructionsSettings) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _pointPieceSpawner = pointPieceSpawner;
            _points = points ?? throw new ArgumentNullException(nameof(points));
            _ghostManager = ghostManager ?? throw new ArgumentNullException(nameof(ghostManager));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _points.OnCurrentLevelUp += HandleLevelChanged;
            _points.OnCurrentLevelDown += HandleLevelChanged;
            _points.OnPointsChanged += HandleOnPointsChanged;
            _points.OnAnimationStarted += Points_OnAnimationStarted;

            _progressBar = new ProgressBarSliders(_view.PointsProgress, _time, constructionsSettings.PointsSliderFrequency, constructionsSettings.PointsSliderSpeed);

            _ghostManager.OnGhostChanged += HandleGhostUpdate;
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

            _ghostManager.OnGhostChanged -= HandleGhostUpdate;
            _ghostManager.OnGhostPostionChanged -= HandleGhostUpdate;
            _points.OnCurrentLevelUp -= HandleLevelChanged;
            _points.OnCurrentLevelDown -= HandleLevelChanged;
            _view.Animator.OnFinished -= HandleAnimationFinished;
            _points.OnAnimationStarted -= Points_OnAnimationStarted;
        }

        private void Points_OnAnimationStarted(AddPointsAnimation obj)
        {
            var curve = new BezierCurve(obj.Postion,
                IPointCounterWidgetView.Default.PointsAttractionPoint.Value,
                obj.Postion + new GameVector3(0, 4, 0),
                IPointCounterWidgetView.Default.PointsAttractionControlPoint.Value);

            new AddPointsAnimationPresenter(curve, _pointPieceSpawner.Presenter, obj);
        }

        private void HandleOnPointsChanged()
        {
            _view.Animator.Play(Animations.Change.ToString(), true);
            UpdateValues();
        }

        private void HandleGhostUpdate()
        {
            SetPotentialPoints(_ghostManager.GetPointChanges());
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
            _view.Points.Value = $"{_points.Value}/{_points.PointsForNextLevel}";
            _progressBar.Value = _points.Progress;

            if (_pointChanges != 0)
            {
                var newPoints = _points.GetChangedValue(_pointChanges);
                if (newPoints.Value < _points.Value)
                {
                    _progressBar.Value = newPoints.Progress;
                    if (newPoints.CurrentLevel == _points.CurrentLevel)
                        _progressBar.Remove = _points.Progress;
                    else
                        _progressBar.Remove = 1;
                    _progressBar.Add = 0;
                }
                else
                {
                    _progressBar.Value = _points.Progress;
                    if (newPoints.CurrentLevel == _points.CurrentLevel)
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
