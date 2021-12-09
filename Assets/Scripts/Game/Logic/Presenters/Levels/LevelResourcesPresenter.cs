using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Orders;
using Game.Assets.Scripts.Game.Logic.Presenters.Units;
using Game.Assets.Scripts.Game.Logic.Views.Game;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Levels
{
    public class LevelResourcesPresenter : Disposable
    {
        private readonly GameLevel _model;
        private readonly ILevelResourcesView _view;

        public LevelResourcesPresenter(GameLevel model, ILevelResourcesView view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.Money.SetValue(0);
            _model.OnMoneyChanged += _model_OnMoneyChanged;
        }

        protected override void DisposeInner()
        {
            _model.OnMoneyChanged -= _model_OnMoneyChanged;
            _view.Dispose();
        }

        private void _model_OnMoneyChanged()
        {
            _view.Money.SetValue(_model.Money);
        }

    }
}
