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
        public ContainerView CellsContainer { get; private set; }
        public PrototypeView Cell { get; private set; }
        public PlacementManagerPresenter Presenter { get; private set; }

        public PlacementManagerView(ILevel level, ContainerView cellsContainer, PrototypeView cellPrototype) : base(level)
        {
            CellsContainer = cellsContainer ?? throw new ArgumentNullException(nameof(cellsContainer));
            Cell = cellPrototype ?? throw new ArgumentNullException(nameof(cellPrototype));
            Presenter = new PlacementManagerPresenter(Level.Model.Constructions, this);
        }
    }
}