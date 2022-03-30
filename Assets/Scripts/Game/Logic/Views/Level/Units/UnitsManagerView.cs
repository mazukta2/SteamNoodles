using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Tests.Environment.Common.Creation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Level
{
    public class UnitsManagerView : View
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab UnitPrototype { get; private set; }

        private UnitsPresenter _presenter;

        public UnitsManagerView(ILevel level, TestContainerView container, TestPrototypeView prototype) : base(level)
        {
            Container = container;
            UnitPrototype = prototype;

            _presenter = new UnitsPresenter(Level.Model.Units, this);
        }
    }
}