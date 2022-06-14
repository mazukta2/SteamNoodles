using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Building;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PlacementFieldUnityView : UnityView<PlacementFieldPresenter>, IPlacementFieldView
    {
        [SerializeField] ContainerUnityView _cellsContainer;
        [SerializeField] PrototypeUnityView _cellsPrototype;
        [SerializeField] ContainerUnityView _constructionContainer;
        [SerializeField] PrototypeUnityView _constructionPrototype;

        public IViewContainer ConstrcutionContainer => _constructionContainer;
        public IViewPrefab ConstrcutionPrototype => _constructionPrototype;
        public IViewContainer CellsContainer => _cellsContainer;
        public IViewPrefab Cell => _cellsPrototype;
    }

}
