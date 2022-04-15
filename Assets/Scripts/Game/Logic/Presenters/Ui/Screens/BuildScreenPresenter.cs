using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BasePresenter<IBuildScreenView>
    {
        public ConstructionCard CurrentCard { get; }

        private IBuildScreenView _view;
        private ScreenManagerPresenter _screenManager;
        private Resources _resources;

        public BuildScreenPresenter(IBuildScreenView view, ScreenManagerPresenter screenManager, Resources resources, ConstructionCard constructionCard) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _resources = resources ?? throw new ArgumentNullException(nameof(resources));
            _view.CancelButton.SetAction(CancelClick);

            CurrentCard = constructionCard;
        }

        protected override void DisposeInner()
        {
        }

        private void CancelClick()
        {
            _screenManager.GetCollection<CommonScreens>().Open<IMainScreenView>();
        }

        public void UpdatePoints(int points)
        {
            _view.Points.Value = $"+{points}";
            _view.CurrentPoints.Value = $"{_resources.Points.Value}/{_resources.Points.PointsForNextLevel}";
            _view.PointsProgress.Value = _resources.Points.Progress;
            _view.PointsProgress.AdditonalValue = _resources.Points.GetAdditionalProgress(points);
        }

        public class BuildScreenCollection : ScreenCollection
        {
            public void Open(ConstructionCard constructionCard)
            {
                Manager.Open<IBuildScreenView>(Init);

                object Init(IBuildScreenView screenView, ScreenManagerPresenter managerPresenter)
                {
                    return new BuildScreenPresenter(screenView, managerPresenter, screenView.Level.Model.Resources, constructionCard);
                }
            }
        }
    }
}
