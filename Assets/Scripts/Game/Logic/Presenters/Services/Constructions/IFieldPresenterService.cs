using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions
{
    public interface IFieldPresenterService
    {
        GameVector3 GetWorldPosition(Construction construction);
        FieldBoundaries GetBoundaries();
        FieldPosition GetWorldConstructionToField(GameVector3 world, IntRect size);
        GameVector3 GetAlignWithAGrid(GameVector3 world, IntRect size);
        GameVector3 GetWorldPosition(FieldPosition position, IntRect sise);
    }
}
