using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Builders;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Screens
{
    public class BuildScreenView : ScreenView
    {
        public ButtonView CancelButton { get; }
        public IText Points { get; }
        public IText CurrentPoints { get; }
        public IProgressBar PointsProgress { get; set; }

        public BuildScreenView(ILevel level, ButtonView cancelButton, IText points, IText currentPoints, IProgressBar progressBar) : base(level)
        {
            CancelButton = cancelButton;
            Points = points;
            PointsProgress = progressBar;
            CurrentPoints = currentPoints;
        }

        private void Init(ScreenManagerPresenter manager, ConstructionCard constructionCard)
        {
            Presenter = new BuildScreenPresenter(this, manager, Level.Model.Resources, constructionCard);
        }

        public class BuildScreenCollection : ScreenCollection
        {
            public void Open(ConstructionCard constructionCard)
            {
                Manager.Open<BuildScreenView>(Init);
                void Init(BuildScreenView screenView, ScreenManagerPresenter managerPresenter)
                {
                    screenView.Init(managerPresenter, constructionCard);
                }
            }
        }
    }
}
