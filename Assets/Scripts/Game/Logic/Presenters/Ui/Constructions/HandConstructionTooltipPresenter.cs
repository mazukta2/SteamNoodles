using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionTooltipPresenter : BasePresenter<IHandConstructionTooltipView>
    {
        private readonly IHandConstructionTooltipView _view;
        private readonly IPresenterRepository<Construction> _constructions;
        private LocalizatedText _name;
        private LocalizatedText _adjacency;
        private IEnumerable<ConstructionScheme> _highlights;
        private ConstructionCard _model;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view) :
                this(view,
                    IPresenterServices.Default?.Get<IPresenterRepository<Construction>>())
        {
        }

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view,
            IPresenterRepository<Construction> constructions) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructions = constructions ?? throw new ArgumentNullException(nameof(constructions));
            UpdateView();
        }

        protected override void DisposeInner()
        {
            _name?.Dispose();
            _adjacency?.Dispose();
        }

        public void SetModel(ConstructionCard card)
        {
            _model = card ?? throw new ArgumentNullException(nameof(card));
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
            _adjacency?.Dispose();
            var model = _model;

            _name = new LocalizatedText(_view.Name, new LocalizatedString(model.Name));
            _view.Points.Value = model.Points.AsString();

            var bonuses = new List<ILocalizatedString>();
            foreach (var (construction, points) in model.AdjacencyPoints.GetAll())
            {
                var style = TextHelpers.TextStyles.None;

                if (_highlights != null && _highlights.Any(x => x.Compare(construction)))
                {
                    style = TextHelpers.TextStyles.HeavyHighlight;
                }
                else if (_constructions.Get().Any(x => x.Scheme.Compare(construction)))
                {
                    style = TextHelpers.TextStyles.Highlight;
                }

                bonuses.Add(new LocalizatedFormatString("{0} ({1})".Style(style),
                    new LocalizatedString(construction.Name), points.AsString()));
            }

            _adjacency = new LocalizatedText(_view.Adjacencies, new LocalizatedJoinString(", ", bonuses.ToArray()));
        }
    }
}
