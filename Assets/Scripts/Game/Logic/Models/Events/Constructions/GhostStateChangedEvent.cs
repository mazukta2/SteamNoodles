﻿using Game.Assets.Scripts.Game.Logic.Common.Services.Events;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Repositories;
using Game.Assets.Scripts.Game.Logic.Presenters.Repositories;
using System;

namespace Game.Assets.Scripts.Game.Logic.Models.Events.Constructions
{
    public record GhostStateChangedEvent() : IEvent;
}