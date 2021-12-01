namespace Game.Assets.Scripts.Game.Logic.Views.Orders
{
    public interface IRecipeView : IView
    {
        void SetName(string name);

        void SetCount(int count);
        void SetMaxCount(int max);
    }
}
