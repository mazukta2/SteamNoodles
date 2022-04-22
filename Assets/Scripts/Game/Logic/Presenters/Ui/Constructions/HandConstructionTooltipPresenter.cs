using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionTooltipPresenter : BasePresenter<IHandConstructionTooltipView>
    {
        private ConstructionCard _model;
        private IHandConstructionTooltipView _view;
        private LocalizatedString _name;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view, ConstructionCard model) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));

            _name = new LocalizatedString(_view.Name, _model.Definition.Name);
            _view.Points.Value = $"+{_model.Definition.Points}";
        }

        protected override void DisposeInner()
        {
            _name.Dispose();
        }
    }
}
