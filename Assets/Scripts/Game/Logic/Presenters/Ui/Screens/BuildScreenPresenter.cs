using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class BuildScreenPresenter : BaseGameScreenPresenter
    {
        public ConstructionCard CurrentCard { get; }

        private BuildScreenView _view;
        private ScreenManagerPresenter _screenManager;

        public BuildScreenPresenter(BuildScreenView view, ScreenManagerPresenter screenManager, ConstructionCard constructionCard) : base(screenManager, view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            _view.CancelButton.SetAction(CancelClick);

            CurrentCard = constructionCard;
        }

        protected override void DisposeInner()
        {
        }

        private void CancelClick()
        {
            _screenManager.GetCollection<CommonScreens>().Open<MainScreenView>();
        }

    }
}
