using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementManagerView : View
    {
        public TestContainerView ConstrcutionContainer { get; private set; }
        public TestPrototypeView ConstrcutionPrototype { get; private set; }
        public TestContainerView CellsContainer { get; private set; }
        public TestPrototypeView Cell { get; private set; }
        public PlacementManagerPresenter Presenter { get; private set; }

        private ServiceWaiter<ScreenManagerService> _wait;

        public PlacementManagerView(ILevel level, TestContainerView cellsContainer, TestPrototypeView cellPrototype,
            TestContainerView constructionContainer, TestPrototypeView constructionPrototype) : base(level)
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