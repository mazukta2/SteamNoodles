using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions
{
    public interface IBuildingPresenterService
    {
        Construction Build(ConstructionCard card, FieldPosition position, FieldRotation rotation);
        bool CanPlace(ConstructionCard card, FieldPosition position, FieldRotation rotation);
        BuildingPoints GetPoints(ConstructionCard card, FieldPosition position, FieldRotation rotation);
        IReadOnlyDictionary<Construction, BuildingPoints> GetAdjacencyPoints(ConstructionCard card, FieldPosition position, FieldRotation rotation);
        IReadOnlyCollection<FieldPosition> GetAllOccupiedSpace();
        bool IsFreeCell(ConstructionScheme scheme, FieldPosition position, FieldRotation rotation);
    }
}
