using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens
{
    public class DayEndedScreenPresenter : BasePresenter<IDayEndedScreenView>, IScreenPresenter
    {
        private IDayEndedScreenView _view;
        private readonly IGameSession _session;
        private ScreenManagerPresenter _screenManager;

        public DayEndedScreenPresenter(IDayEndedScreenView view, IGameSession session, ScreenManagerPresenter screenManager) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _session = session ?? throw new ArgumentNullException(nameof(session));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            _view.NextDayButton.SetAction(NextDayClick);
        }

        protected override void DisposeInner()
        {
        }

        private void NextDayClick()
        {
            //_session.StartLastAvailableLevel();
        }

        public void Close()
        {
        }
    }
}
