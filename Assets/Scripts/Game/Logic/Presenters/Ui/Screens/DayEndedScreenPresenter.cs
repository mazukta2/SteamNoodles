using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class DayEndedScreenPresenter : BasePresenter<IDayEndedScreenView>
    {
        private IDayEndedScreenView _view;
        private ScreenManagerPresenter _screenManager;

        public DayEndedScreenPresenter(IDayEndedScreenView view, ScreenManagerPresenter screenManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            _view.NextDayButton.SetAction(NextDayClick);
        }

        protected override void DisposeInner()
        {
        }

        private void NextDayClick()
        {
        }
    }
}
