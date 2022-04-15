using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using System;

namespace Game.Assets.Scripts.Tests.Environment.Views.Level.Building
{
    public class PlacementManagerView : PresenterView<PlacementManagerPresenter>, IPlacementManagerView
    {
        public IViewContainer ConstrcutionContainer { get; private set; }
        public IViewPrefab ConstrcutionPrototype { get; private set; }
        public IViewContainer CellsContainer { get; private set; }
        public IViewPrefab Cell { get; private set; }

        public PlacementManagerView(LevelView level, IViewContainer cellsContainer, IViewPrefab cellPrototype,
            IViewContainer constructionContainer, IViewPrefab constructionPrototype) : base(level)
        {
            CellsContainer = cellsContainer ?? throw new ArgumentNullException(nameof(cellsContainer));
            Cell = cellPrototype ?? throw new ArgumentNullException(nameof(cellPrototype));
            ConstrcutionContainer = constructionContainer ?? throw new ArgumentNullException(nameof(constructionContainer));
            ConstrcutionPrototype = constructionPrototype ?? throw new ArgumentNullException(nameof(constructionPrototype));
        }
    }
}