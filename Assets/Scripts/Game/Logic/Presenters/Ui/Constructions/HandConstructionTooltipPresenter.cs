using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionTooltipPresenter : BasePresenter<IHandConstructionTooltipView>
    {
        private ConstructionCard _model;
        private IHandConstructionTooltipView _view;
        private PlacementField _field;
        private LocalizatedText _name;
        private LocalizatedText _adjecensy;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view, ConstructionCard model, PlacementField field) : base(view)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _field = field ?? throw new ArgumentNullException(nameof(field));

            _name = new LocalizatedText(_view.Name, new LocalizatedString(_model.Definition.Name));
            _view.Points.Value = $"+{_model.Definition.Points}";

            var bonuses = new List<ILocalizatedString>();
            foreach (var bonus in _model.Definition.AdjacencyPoints)
            {
                var hasConstrcution = _field.Constructions.Any(x => x.Definition == bonus.Key);
                bonuses.Add(new LocalizatedFormatString("{0} ({1})"
                     .Style(hasConstrcution ? TextHelpers.TextStyles.Highlight : TextHelpers.TextStyles.None),
                    new LocalizatedString(bonus.Key.Name), bonus.Value.GetSignedNumber()));
            }

            _adjecensy = new LocalizatedText(_view.Adjacencies, new LocalizatedJoinString(", ", bonuses.ToArray()));
        }

        protected override void DisposeInner()
        {
            _name.Dispose();
            _adjecensy.Dispose();
        }
    }
}
