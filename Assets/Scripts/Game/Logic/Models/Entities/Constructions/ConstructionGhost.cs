﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Events.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;

namespace Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions
{
    public record ConstructionGhost : Entity
    {
        public ConstructionCard Card { get; }
        public FieldPosition Position { get; private set; }
        public FieldRotation Rotation { get; private set;}
        public GameVector3 TargetPosition { get; private set;}
        public BuildingPoints PointChanges { get; private set; }

        public ConstructionGhost(ConstructionCard card, Field field) : this(card, new FieldPosition(field))
        {
            
        }

        public ConstructionGhost(ConstructionCard card, FieldPosition position) : this(card, position, GameVector3.Zero, FieldRotation.Default)
        {
            
        }
        
        public ConstructionGhost(ConstructionCard card, FieldPosition position, GameVector3 targetPosition, FieldRotation rotation)
        {
            Card = card;
            Position = position;
            Rotation = rotation;
            TargetPosition = targetPosition;
            PointChanges = BuildingPoints.Zero;
        }

        public void SetPosition(FieldPosition cellPosition, GameVector3 pointerPosition)
        {
            TargetPosition = pointerPosition;
            if (Position != cellPosition)
            {
                Position = cellPosition;
                FireEvent(new GhostMovedEvent());
            }
        }

        public void SetRotation(FieldRotation rotation)
        {
            Rotation = rotation;
            FireEvent(new GhostMovedEvent());
        }

        public void SetPoints(BuildingPoints points)
        {
            PointChanges = points;
            FireEvent(new GhostPointsChangedEvent());
        }
    }
}
