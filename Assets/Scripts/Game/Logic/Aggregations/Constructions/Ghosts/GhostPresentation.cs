using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Fields;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts
{
    public class GhostPresentation : PrivateDisposable, IAggregation
    {

        public Uid Id { get; }
        public Uid CardId { get; }

        public IReadOnlyCollection<FieldPosition> GetOccupiedSpace()
        {
            throw new NotImplementedException();
        }
        // public bool CanBuild { get; private set; }
        // public BuildingPoints Points { get; private set; }
        // public FieldRotation Rotation { get; private set; }
        // public FieldPosition Position { get;  private set; }
        // public GameVector3 TargetPosition { get; private set; }
        // public GameVector3 WorldPosition { get; private set; }
        // public IDataProvider<ConstructionCardData> CardData { get; private set; }
        // public IViewPrefab Prefab { get; set; }
        // public IReadOnlyCollection<FieldPosition> OccupiedSpace { get; private set; }
        // // public ConstructionCard Card { get; set; }
        //
        // private Field _field;
        // private ContructionPlacement _placement;
        //
        // public static GhostData Default(Field field)
        // {
        //     var data = new GhostData();
        //     data._field = field;
        //     data._placement = ContructionPlacement.One;
        //     data.Points = new BuildingPoints(0);
        //     data.Position = new FieldPosition(field,0, 0);
        //     data.Rotation = new FieldRotation();
        //     data.TargetPosition = GameVector3.Zero;
        //     data.CardData = new DataProvider<ConstructionCardData>(ConstructionCardData.Default());
        //     data.OccupiedSpace = data._placement.GetOccupiedSpace(data.Position, data.Rotation);
        //     return data;
        // }
        //
        // public GhostData SetPosition(GameVector3 targetPosition)
        // {
        //     Position =_field.GetFieldPosition(targetPosition, CardData.Get().Size);
        //     TargetPosition = targetPosition;
        //     WorldPosition = Position.GetWorldPosition(CardData.Get().Size);
        //     OccupiedSpace = _placement.GetOccupiedSpace(Position, Rotation);
        //     return this;
        // }
        public BuildingPoints GetPoints()
        {
            throw new NotImplementedException();
        }

        public GameVector3 GetWorldPosition()
        {
            throw new NotImplementedException();
        }

        public GameVector3 GetTargetPosition()
        {
            throw new NotImplementedException();
        }

        public IViewPrefab GetPrefab()
        {
            throw new NotImplementedException();
        }

        public FieldRotation GetRotation()
        {
            throw new NotImplementedException();
        }

        public bool GetCanBuild()
        {
            throw new NotImplementedException();
        }
    }
}