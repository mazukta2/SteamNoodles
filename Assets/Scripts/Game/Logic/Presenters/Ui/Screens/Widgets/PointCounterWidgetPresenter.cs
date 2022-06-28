using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions;
using System;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points.BuildingPointsAnimations;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class PointCounterWidgetPresenter : BasePresenter<IPointCounterWidgetView>
    {
        private readonly IPointCounterWidgetView _view;
        private BuildingPointsService _points;
        private ProgressBarSliders _progressBar;
        private readonly GhostRepository _ghost;

        public PointCounterWidgetPresenter(IPointCounterWidgetView view)
            : this(view,
                  IPresenterServices.Default.Get<DefinitionsService>().Get<ConstructionsSettingsDefinition>())
        {

        }

        public PointCounterWidgetPresenter(IPointCounterWidgetView view, 
            ConstructionsSettingsDefinition constructionsSettingsDefinition) 
            : this(view, 
                  new ProgressBarSliders(view.PointsProgress, IGameTime.Default,
                      constructionsSettingsDefinition.PointsSliderFrequency,
                      constructionsSettingsDefinition.PointsSliderSpeed),
                  IPresenterServices.Default.Get<GhostRepository>(),
                  IPresenterServices.Default.Get<BuildingPointsService>())
        {

        }

        public PointCounterWidgetPresenter(IPointCounterWidgetView view,
            ProgressBarSliders progressBar,
            GhostRepository ghost,
            BuildingPointsService pointsService) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _progressBar = progressBar ?? throw new ArgumentNullException(nameof(progressBar));
            _ghost = ghost ?? throw new ArgumentNullException(nameof(ghost));
            _points = pointsService ?? throw new ArgumentNullException(nameof(pointsService));

            _points.OnCurrentLevelUp += HandleLevelChanged;
            _points.OnCurrentLevelDown += HandleLevelChanged;
            _points.OnPointsChanged += HandleOnPointsChanged;
            _points.OnAnimationStarted += Points_OnAnimationStarted;

            // _ghost.OnAdded += UpdateValues;
            // _ghost.OnRemoved += UpdateValues;

            _view.PointsProgress.RemovedValue = 0;
            _view.PointsProgress.AddedValue = 0;
            _view.PointsProgress.MainValue = 0;
            
            UpdateValues();
        }

        protected override void DisposeInner()
        {
            _progressBar.Dispose();

            _points.OnCurrentLevelUp -= HandleLevelChanged;
            _points.OnCurrentLevelDown -= HandleLevelChanged;
            _view.Animator.OnFinished -= HandleAnimationFinished;
            _points.OnAnimationStarted -= Points_OnAnimationStarted;
            
            // _ghost.OnAdded -= UpdateValues;
            // _ghost.OnRemoved -= UpdateValues;
        }

        private void Points_OnAnimationStarted(AddPointsAnimation obj)
        {
            //var curve = new BezierCurve(obj.Postion,
            //    IPointCounterWidgetView.Default.PointsAttractionPoint.Value,
            //    obj.Postion + new GameVector3(0, 4, 0),
            //    IPointCounterWidgetView.Default.PointsAttractionControlPoint.Value);

            //new AddPointsAnimationPresenter(curve, _pointPieceSpawner.Presenter, obj);
        }

        private void HandleOnPointsChanged()
        {
            _view.Animator.Play(Animations.Change.ToString(), true);
            UpdateValues();
        }

        private void HandleLevelChanged()
        {
            _view.Animator.Play(Animations.Level.ToString(), true);
            _progressBar.Skip();
        }

        private void UpdateValues()
        {
            _view.Points.Value = $"{_points.GetValue()}/{_points.GetPointsForNextLevel()}";
            _progressBar.Value = _points.GetProgress();

            // if (_ghost.Has())
            // {
            //     var ghost = _ghost.Get();
            //     var newPoints = _points.GetChangedValue(ghost.GetPoints().AsInt());
            //     if (newPoints.Value < _points.GetValue())
            //     {
            //         _progressBar.Value = newPoints.Progress;
            //         if (newPoints.CurrentLevel == _points.GetCurrentLevel().Value)
            //             _progressBar.Remove = _points.GetProgress();
            //         else
            //             _progressBar.Remove = 1;
            //         _progressBar.Add = 0;
            //     }
            //     else
            //     {
            //         _progressBar.Value = _points.GetProgress();
            //         if (newPoints.CurrentLevel == _points.GetCurrentLevel().Value)
            //             _progressBar.Add = newPoints.Progress;
            //         else
            //             _progressBar.Add = 1;
            //         _progressBar.Remove = 0;
            //     }
            // }
            // else
            // {
            //     _progressBar.Add = 0;
            //     _progressBar.Remove = 0;
            // }

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
