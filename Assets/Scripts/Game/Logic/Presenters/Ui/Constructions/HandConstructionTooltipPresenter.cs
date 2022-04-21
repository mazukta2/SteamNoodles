using Game.Assets.Scripts.Game.Environment.Engine.Controls;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.BuildScreenPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionTooltipPresenter : BasePresenter<IHandConstructionTooltipView>
    {
        private ConstructionCard _model;
        private IHandConstructionTooltipView _view;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view, ConstructionCard model) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _view.Name.Value = "TEST";
        }

    }
}
