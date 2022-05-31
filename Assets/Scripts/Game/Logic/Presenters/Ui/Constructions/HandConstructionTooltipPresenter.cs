using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
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
        private readonly IPresenterRepository<Construction> _constructions;
        private LocalizatedText _name;
        private LocalizatedText _adjecensy;
        private IEnumerable<ConstructionScheme> _highlights;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view, IPresenterRepository<Construction> constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            UpdateView();
        }

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view, IPresenterRepository<Construction> constructions, ConstructionCard model) : this(view, constructions)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            UpdateView();
        }

        public void SetModel(ConstructionCard model)
        {
            _model = model ?? throw new ArgumentNullException(nameof(model));
            UpdateView();
        }

        public void SetHighlight(IEnumerable<ConstructionScheme> highlights)
        {
            if (highlights != null && _highlights != null && highlights.Equals(_highlights))
                return;

            _highlights = highlights;
            UpdateView();
        }

        private void UpdateView()
        {
            if (_model == null)
                return;

            _name?.Dispose();
            _adjecensy?.Dispose();

            _name = new LocalizatedText(_view.Name, new LocalizatedString(_model.Name));
            _view.Points.Value = $"+{_model.Points}";

            var bonuses = new List<ILocalizatedString>();
            foreach (var (construction, points) in _model.AdjacencyPoints.GetAll())
            {
                var style = TextHelpers.TextStyles.None;

                if (_highlights != null && _highlights.Any(x => x.Compare(construction)))
                {
                    style = TextHelpers.TextStyles.HeavyHighlight;
                }
                else if (_constructions.GetAll().Any(x => x.Scheme.Compare(construction)))
                {
                    style = TextHelpers.TextStyles.Highlight;
                }

                bonuses.Add(new LocalizatedFormatString("{0} ({1})".Style(style),
                    new LocalizatedString(construction.Name), points.Value.GetSignedNumber()));
            }

            _adjecensy = new LocalizatedText(_view.Adjacencies, new LocalizatedJoinString(", ", bonuses.ToArray()));
        }

        protected override void DisposeInner()
        {
            _name?.Dispose();
            _adjecensy?.Dispose();
        }
    }
}
