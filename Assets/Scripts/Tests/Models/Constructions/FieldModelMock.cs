using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Tests.Models.Constructions
{
    public class FieldModelMock : Disposable, IFieldModel
    {
        Dictionary<IntPoint, CellPlacementStatus> _status;
        Dictionary<IntPoint, GameVector3> _positions;

        public FieldModelMock(Dictionary<IntPoint, CellPlacementStatus> list,
            Dictionary<IntPoint, GameVector3> positions, FieldBoundaries boundaries)
        {
            _status = list;
            _positions = positions;
            Boudaries = boundaries;
        }

        public FieldModelMock()
        {
            _status = new Dictionary<IntPoint, CellPlacementStatus>() { { IntPoint.Zero, CellPlacementStatus.Normal } };
            _positions = new Dictionary<IntPoint, GameVector3>() { { IntPoint.Zero, GameVector3.Zero } };
            Boudaries = new FieldBoundaries(IntPoint.One);
        }

        public FieldBoundaries Boudaries { get; private set; }

        public event Action OnUpdate = delegate { };
        public event Action<Uid> OnConstructionBuilded = delegate { };

        public GameVector3 GetCellWorldPosition(IntPoint position)
        {
            return _positions[position];
        }

        public ConstructionPresenter CreatePresenter(IConstructionView view, Uid id)
        {
            return null;
        }

        public void FireOnConstructionBuilded(Uid id)
        {
            OnConstructionBuilded(id);
        }

        public CellPlacementStatus GetStatus(IntPoint position)
        {
            return _status[position];
        }
    }
}
