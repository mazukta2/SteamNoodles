using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services;
using Game.Assets.Scripts.Game.Logic.Common.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets.Scripts.Game.Logic.Models.Services.Requests
{
    public class FieldRequestsService : IService,
        IRequestHandler<GetField>
    {
        private readonly FieldService _fieldService;
        private readonly ConstructionsService _constructionsService;
        private readonly BuildingModeService _modeService;

        public FieldRequestsService(FieldService fieldService,
            ConstructionsService constructionsService, BuildingModeService modeService)
        {
            _fieldService = fieldService ?? throw new ArgumentNullException(nameof(fieldService));
            _constructionsService = constructionsService ?? throw new ArgumentNullException(nameof(constructionsService));
            _modeService = modeService ?? throw new ArgumentNullException(nameof(modeService));
        }

        public void Handle(GetField request)
        {
            var occupiedCells = _constructionsService.GetAllOccupiedSpace();
            IReadOnlyCollection<FieldPosition> ocuppiedCellsByGhost = null;
            if (_modeService.IsEnabled)
            {
                var scheme = _modeService.Card.Scheme;
                ocuppiedCellsByGhost = scheme.Placement.GetOccupiedSpace(_modeService.GetPosition(), _modeService.GetRotation());
            }

            var occupiedByBuildings = _constructionsService.GetAllOccupiedSpace();
            var boundaries = _fieldService.GetBoundaries();
            var list = new Dictionary<IntPoint, CellPlacementStatus>();
            var positions = new Dictionary<IntPoint, GameVector3>();
            for (int x = boundaries.Value.xMin; x <= boundaries.Value.xMax; x++)
            {
                for (int y = boundaries.Value.yMin; y <= boundaries.Value.yMax; y++)
                {
                    var state = CellPlacementStatus.Normal;

                    if (occupiedByBuildings.Any(v => v.Value == new IntPoint(x, y)))
                        state = CellPlacementStatus.IsUnderConstruction;

                    if (_modeService.IsEnabled)
                    {
                        var scheme = _modeService.Card.Scheme;
                        if (_constructionsService.IsFreeCell(scheme, new FieldPosition(x, y), _modeService.GetRotation()))
                            state = CellPlacementStatus.IsReadyToPlace;

                        if (ocuppiedCellsByGhost.Any(v => v.Value == new IntPoint(x, y)))
                        {
                            if (state == CellPlacementStatus.IsReadyToPlace)
                                state = CellPlacementStatus.IsAvailableGhostPlace;
                            else
                                state = CellPlacementStatus.IsNotAvailableGhostPlace;
                        }
                    }
                    list.Add(new IntPoint(x, y), state);
                    positions.Add(new IntPoint(x, y),
                        _fieldService.GetWorldPosition(new FieldPosition(new IntPoint(x, y)), new IntRect(0, 0, 1, 1)));
                }
            }

            request.Respond.SetCells(list, positions, boundaries);
        }
    }
}
