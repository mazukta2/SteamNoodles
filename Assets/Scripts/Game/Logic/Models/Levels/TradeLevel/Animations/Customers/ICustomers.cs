﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers
{
    public interface ICustomers
    {
        GameVector3 GetQueueFirstPosition();
        GameVector3 GetQueueFirstPositionOffset();
        float SpawnAnimationDelay { get; }
        int GetQueueSize();
        void Serve(Unit unit);
    }
}