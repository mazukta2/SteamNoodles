using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class PlacementManagerView : View
    {
        public ContainerView ConstrcutionContainer { get; private set; }
        public ContainerView CellsContainer { get; private set; }
        public PrototypeView Cell { get; private set; }
        public PlacementManagerPresenter Presenter { get; private set; }

        private ServiceWaiter<ScreenManagerService> _wait;

        public PlacementManagerView(ILevel level, ContainerView cellsContainer, PrototypeView cellPrototype,
            ContainerView constructionContainer) : base(level)
        {
            CellsContainer = cellsContainer ?? throw new ArgumentNullException(nameof(cellsContainer));
            Cell = cellPrototype ?? throw new ArgumentNullException(nameof(cellPrototype));
            ConstrcutionContainer = constructionContainer ?? throw new ArgumentNullException(nameof(constructionContainer));
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