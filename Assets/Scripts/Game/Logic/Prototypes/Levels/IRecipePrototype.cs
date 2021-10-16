namespace Assets.Scripts.Logic.Prototypes.Levels
{
    public interface IRecipePrototype
    {
        IIngredientPrototype Ingredient { get; }
        int Count { get; }
    }
}
