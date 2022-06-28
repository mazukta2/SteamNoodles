﻿using Game.Assets.Scripts.Game.Logic.Common.Helpers;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Services.Repositories;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class HandConstructionTooltipPresenter : BasePresenter<IHandConstructionTooltipView>
    {
        private readonly IHandConstructionTooltipView _view;
        private readonly IDataCollectionProvider<ConstructionPresenterData> _constructions;
        private LocalizatedText _name;
        private LocalizatedText _adjacency;
        private IEnumerable<ConstructionScheme> _highlights;
        private IDataProvider<ConstructionCardData> _model;

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view) :
                this(view,
                    IPresenterServices.Default?.Get<IDataCollectionProviderService<ConstructionPresenterData>>().Get())
        {
        }

        public HandConstructionTooltipPresenter(IHandConstructionTooltipView view,
            IDataCollectionProvider<ConstructionPresenterData> constructions) : base(view)
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

        public void SetModel(Uid cardId)
        {
            ///_model = card ?? throw new ArgumentNullException(nameof(card));
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

            _name = new LocalizatedText(_view.Name, new LocalizatedString(model.Get().Name));
            _view.Points.Value = model.Get().Points.AsString();

            var bonuses = new List<ILocalizatedString>();
            foreach (var (construction, points) in model.Get().AdjacencyPoints.GetAll())
            {
                var style = TextHelpers.TextStyles.None;

                if (_highlights != null && _highlights.Any(x => x.Compare(construction)))
                {
                    style = TextHelpers.TextStyles.HeavyHighlight;
                }
                else if (_constructions.Get().Any(x => x.Get().Scheme.Compare(construction)))
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
