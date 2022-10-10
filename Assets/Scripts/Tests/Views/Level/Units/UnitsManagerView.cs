﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Units;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Common.Creation;

namespace Game.Assets.Scripts.Tests.Views.Level.Units
{
    public class UnitsManagerView : ViewWithPresenter<UnitsPresenter>, IUnitsManagerView
    {
        public IViewContainer Container { get; private set; }
        public IViewPrefab UnitPrototype { get; private set; }

        public UnitsManagerView(IViews level) : base(level)
        {
            Container = new ContainerViewMock(level);
            UnitPrototype = new UnitViewPrefab();
        }

        private class UnitViewPrefab : ViewCollectionPrefabMock
        {
            public override void Fill(IViews collection)
            {
                new UnitView(collection, new Rotator(), new AnimatorMock(), new UnitDresser());
            }
        }
    }
}