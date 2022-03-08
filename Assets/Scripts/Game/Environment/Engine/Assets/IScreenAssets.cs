﻿using Game.Assets.Scripts.Game.Logic.Common.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views.Ui;

namespace Game.Assets.Scripts.Game.Environment.Engine.Assets
{
    public interface IScreenAssets
    {
        IScreenAsset<T> GetScreen<T>() where T : ScreenView;
    }
}