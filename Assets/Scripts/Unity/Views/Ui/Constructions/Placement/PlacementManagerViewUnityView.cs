using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Unity.Views;
using UnityEngine;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class PlacementManagerViewUnityView : UnityView<PlacementManagerView>
    {
        [SerializeField] ContainerUnityView _cellsContainer;
        [SerializeField] PrototypeUnityView _cellsPrototype;
        [SerializeField] ContainerUnityView _constructionContainer;

        protected override PlacementManagerView CreateView()
        {
            return new PlacementManagerView(Level, _cellsContainer.View, _cellsPrototype.View, _constructionContainer.View);
        }

    }

}
