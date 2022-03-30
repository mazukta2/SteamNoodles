using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using System;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementManagerView : View
    {
        public IViewContainer ConstrcutionContainer { get; private set; }
        public IViewPrefab ConstrcutionPrototype { get; private set; }
        public IViewContainer CellsContainer { get; private set; }
        public IViewPrefab Cell { get; private set; }
        public PlacementManagerPresenter Presenter { get; private set; }

        private ServiceWaiter<ScreenManagerService> _wait;

        public PlacementManagerView(ILevel level, IViewContainer cellsContainer, IViewPrefab cellPrototype,
            IViewContainer constructionContainer, IViewPrefab constructionPrototype) : base(level)
        {
            CellsContainer = cellsContainer ?? throw new ArgumentNullException(nameof(cellsContainer));
            Cell = cellPrototype ?? throw new ArgumentNullException(nameof(cellPrototype));
            ConstrcutionContainer = constructionContainer ?? throw new ArgumentNullException(nameof(constructionContainer));
            ConstrcutionPrototype = constructionPrototype ?? throw new ArgumentNullException(nameof(constructionPrototype));
            _wait = level.Services
                .Request<ScreenManagerService>(Load);
        }

        protected override void DisposeInner()
        {
            base.DisposeInner();
            _wait.Dispose();
        }

        private void Load(ScreenManagerService screenManager)
        {
            Presenter = new PlacementManagerPresenter(Level.Model.Constructions, screenManager.Get(), this);
        }
    }
}