using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Unity.Views.Ui.Screens
{
    public class BuildScreenUnityView : ScreenUnityView<BuildScreenPresenter>, IBuildScreenView
    {
        [SerializeField] ButtonUnityView _closeButton;
        [SerializeField] UnityWorldText _additionalPoints;
        [SerializeField] ContainerUnityView _adjacencyConteiner;
        [SerializeField] PrototypeUnityView _adjacencyPrefab;

        public IButton CancelButton => _closeButton;
        public IWorldText Points => _additionalPoints;
        public IViewContainer AdjacencyContainer => _adjacencyConteiner;
        public IViewPrefab AdjacencyPrefab => _adjacencyPrefab;

    }

}
