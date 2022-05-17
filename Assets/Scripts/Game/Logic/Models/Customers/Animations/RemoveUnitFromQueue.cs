using Game.Assets.Scripts.Game.Logic.Models.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Customers.Animations
{
    public class RemoveUnitFromQueue : BaseQueueStep
    {
        private Unit _unit;
        private ICrowd _crowd;
        private readonly Action<Unit> _removeFromQueue;

        public RemoveUnitFromQueue(Unit unit, ICrowd crowd, Action<Unit> removeFromQueue)
        {
            _unit = unit;
            _crowd = crowd;
            _removeFromQueue = removeFromQueue;
        }

        public override void Play()
        {
            _removeFromQueue(_unit);
            _crowd.SendToCrowd(_unit, LevelCrowd.CrowdDirection.Right);
            FireOnFinished();
        }
    }
}
