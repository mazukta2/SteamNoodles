using Game.Assets.Scripts.Game.External;
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
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Animations;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class PointCounterWidgetPresenter : BasePresenter<IPointCounterWidgetView>
    {
        private readonly IPointCounterWidgetView _view;
        private readonly IPointPieceSpawnerView _pointPieceSpawner;
        private Resources _resources;
        private GhostManagerPresenter _ghostManager;
        private PlacementField _placementField;
        private readonly IGameTime _time;
        private ProgressBarSliders _progressBar;
        private int _pointChanges;

        public PointCounterWidgetPresenter(Resources resources, GhostManagerPresenter ghostManager,
            PlacementField placementField, IGameTime time, IPointCounterWidgetView view, IPointPieceSpawnerView pointPieceSpawner) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _pointPieceSpawner = pointPieceSpawner;
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _ghostManager = ghostManager ?? throw new ArgumentNullException(nameof(ghostManager));
            _placementField = placementField ?? throw new ArgumentNullException(nameof(placementField));
            _time = time ?? throw new ArgumentNullException(nameof(time));
            _resources.Points.OnLevelUp += HandleLevelChanged;
            _resources.Points.OnLevelDown += HandleLevelChanged;
            _resources.Points.OnPointsChanged += HandleOnPointsChanged;
            _resources.Points.OnAnimationStarted += Points_OnAnimationStarted;

            var def = IGameDefinitions.Default.Get<ConstructionsSettingsDefinition>();
            _progressBar = new ProgressBarSliders(_view.PointsProgress, _time, def.PointsSliderFrequency, def.PointsSliderSpeed);

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
            _resources.Points.OnLevelUp -= HandleLevelChanged;
            _resources.Points.OnLevelDown -= HandleLevelChanged;
            _view.Animator.OnFinished -= HandleAnimationFinished;
            _resources.Points.OnAnimationStarted -= Points_OnAnimationStarted;
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
            _view.Points.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";
            _progressBar.Value = _resources.Points.Progress;

            if (_pointChanges != 0)
            {
                var newPoints = _resources.Points.GetChangedValue(_pointChanges);
                if (newPoints.Value < _resources.Points.Value)
                {
                    _progressBar.Value = newPoints.Progress;
                    if (newPoints.CurrentLevel == _resources.Points.CurrentLevel)
                        _progressBar.Remove = _resources.Points.Progress;
                    else
                        _progressBar.Remove = 1;
                    _progressBar.Add = 0;
                }
                else
                {
                    _progressBar.Value = _resources.Points.Progress;
                    if (newPoints.CurrentLevel == _resources.Points.CurrentLevel)
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
