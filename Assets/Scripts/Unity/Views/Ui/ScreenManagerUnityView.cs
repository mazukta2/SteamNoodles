using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui;
using Game.Assets.Scripts.Game.Logic.Services.Ui;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerUnityView : UnityView<ScreenManagerPresenter>, IScreenManagerView
    {
        [SerializeField] ContainerUnityView _screen;

        IViewContainer IScreenManagerView.Screen => _screen;
    }
}
