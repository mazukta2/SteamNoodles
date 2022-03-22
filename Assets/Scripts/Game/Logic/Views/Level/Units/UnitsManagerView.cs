using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class UnitsManagerView : View
    {
        public ContainerView Container { get; private set; }
        public PrototypeView UnitPrototype { get; private set; }

        private UnitsPresenter _presenter;

        public UnitsManagerView(ILevel level, ContainerView container, PrototypeView prototype) : base(level)
        {
            Container = container;
            UnitPrototype = prototype;

            _presenter = new UnitsPresenter(Level.Model.Units, this);
        }
    }
}