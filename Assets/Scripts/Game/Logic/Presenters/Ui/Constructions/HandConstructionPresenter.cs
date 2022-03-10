using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.ViewPresenters.Ui.Screens;
using Game.Assets.Scripts.Game.Unity.Views;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionPresenter : BasePresenter
    {
        private ConstructionCard _model;
        private HandConstructionViewPresenter _view;
        private ScreenManagerPresenter _screenManager;

        public HandConstructionPresenter(ScreenManagerPresenter screenManager,  HandConstructionViewPresenter view, ConstructionCard model) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));

            view.Button.SetAction(HandleClick);
            UpdateView();
        }

        private void HandleClick()
        {
            _screenManager.GetScreen<BuildScreenViewPresenter>().Open();
        }

        private void UpdateView()
        {
            //_view.SetIcon(_model.HandIcon);
            //_view.Button.SetAction(OnClick);
        }

        //public bool Is(ConstructionCard obj)
        //{
        //    return _model == obj;
        //}
    }
}
