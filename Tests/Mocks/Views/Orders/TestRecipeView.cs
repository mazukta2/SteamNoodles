using Game.Assets.Scripts.Game.Logic.Views.Orders;
using Tests.Tests.Mocks.Views.Common;

namespace Tests.Tests.Mocks.Views.Levels
{
    public class TestRecipeView : TestView, IRecipeView
    {
        public string Name { get; private set; }
        public int Count { get; private set; }
        public int MaxCount { get; private set; }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetCount(int count)
        {
            Count = count;
        }
        
        public void SetMaxCount(int max)
        {
            MaxCount = max;
        }
    }
}
