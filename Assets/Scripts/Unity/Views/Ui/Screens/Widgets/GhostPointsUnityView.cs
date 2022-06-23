using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Unity.Views;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameUnity.Assets.Scripts.Unity.Views.Ui.Screens.Widgets
{
    public class GhostPointsUnityView : UnityView<GhostPointPresenter>, IGhostPointsView
    {
        [SerializeField] UnityWorldText _text;
        [SerializeField] ContainerUnityView _adjacencyConteiner;
        [SerializeField] PrototypeUnityView _adjacencyPrefab;

        public IWorldText Points => _text;
        public IViewContainer AdjacencyContainer => _adjacencyConteiner;
        public IViewPrefab AdjacencyPrefab => _adjacencyPrefab;
    }
}
