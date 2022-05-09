﻿using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;

namespace Game.Assets.Scripts.Game.Logic.Views.Levels.Building
{
    public interface IPointPieceSpawner : IViewWithPresenter, IViewWithAutoInit, IViewWithGenericPresenter<PointPieceSpawnerPresenter>
    {
        IViewContainer Container { get; }
        IViewPrefab PiecePrefab { get; }
        void IViewWithAutoInit.Init()
        {
            Presenter = new PointPieceSpawnerPresenter(this);
        }
    }
}