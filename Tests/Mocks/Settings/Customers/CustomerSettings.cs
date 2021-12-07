using Assets.Scripts.Logic.Prototypes.Levels;
using System.Collections.Generic;

namespace Tests.Mocks.Prototypes.Levels
{
    public class CustomerSettings : ICustomerSettings
    {
        public int Money { get; set; } = 1;
    }
}
