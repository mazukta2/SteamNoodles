using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui
{
    public class ScreenManagerUnityView : UnitySimpleView, IScreenManagerView
    {
        [SerializeField] ContainerUnityView _screen;

        IViewContainer IScreenManagerView.Screen => _screen;
}
    }
