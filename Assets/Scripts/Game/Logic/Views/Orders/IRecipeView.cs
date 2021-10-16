using Assets.Scripts.Models.Buildings;
using Game.Assets.Scripts.Game.Logic.Views;
using System.Numerics;
using Tests.Assets.Scripts.Game.Logic.ViewModel.Constructions.Placements;
using Tests.Assets.Scripts.Game.Logic.Views.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Views.Orders
{
    public interface IRecipeView : IView
    {
        void SetName(string name);

        void SetCount(int count);
        void SetMaxCount(int max);
    }
}
