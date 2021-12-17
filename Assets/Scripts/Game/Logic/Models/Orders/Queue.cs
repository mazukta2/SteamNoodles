using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Prototypes.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Models.Orders
{
    public class CustomersQueue
    {
        private IClashesSettings _clashesSettings;
        private SessionRandom _random;
        private List<ServingCustomerProcess> _customers = new List<ServingCustomerProcess>();

        public int TargetCount { get; private set; }
        public int ActualCount => _customers.Count;

        public CustomersQueue(IClashesSettings clashesSettings, SessionRandom random)
        {
            _clashesSettings = clashesSettings;
            _random = random;
            RefreshTargetCount();
        }

        private void RefreshTargetCount()
        {
            TargetCount = _random.GetRandom(_clashesSettings.MinQueue, _clashesSettings.MaxQueue);
        }
    }
}
