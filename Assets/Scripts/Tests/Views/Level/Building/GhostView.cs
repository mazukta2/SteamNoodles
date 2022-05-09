using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Settings.Convertion.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Services;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Game.Assets.Scripts.Tests.Views.Level.Building
{
    public class GhostView : PresenterView<GhostPresenter>, IGhostView
    {
        public ILevelPosition LocalPosition { get; private set; }
        public IViewContainer Container { get; private set; }
        public IRotator Rotator { get; }
        public IPointPieceSpawner PieceSpawner { get; }

        public GhostView(LevelView level, IViewContainer container, ILevelPosition position, IRotator rotator) : base(level)
        {
            LocalPosition = position;
            Container = container;
            Rotator = rotator;
            PieceSpawner = new PieceSpawnerView(level);
        }
    }
}
