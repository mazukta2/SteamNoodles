using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using System;
using System.Collections.Generic;

namespace Game.Assets.Scripts.Game.Logic.Models.Models.Consturctions
{
    public class FieldModel : Disposable, IFieldModel
    {
        public event Action OnUpdate = delegate { };
        public event Action<Uid> OnConstructionBuilded = delegate { };

        private FieldRequestsService _requestsService;

        public FieldModel(FieldRequestsService requestsService)
        {
            _requestsService = requestsService;
            _requestsService.OnUpdate += HandleOnUpdate;
        }

        protected override void DisposeInner()
        {
            _requestsService.OnUpdate -= HandleOnUpdate;
        }

        public FieldBoundaries Boudaries => _requestsService.GetBoundaries();

        public GameVector3 GetCellWorldPosition(IntPoint position)
        {
            return _requestsService.GetCells()[position].WorldPosition;
        }

        public CellPlacementStatus GetStatus(IntPoint position)
        {
            return _requestsService.GetCells()[position].Status;
        }

        public ConstructionPresenter CreatePresenter(IConstructionView view, Uid id)
        {
            return new ConstructionPresenter(view, id);
        }

        private void HandleOnUpdate()
        {
            OnUpdate();
        }

    }
}
