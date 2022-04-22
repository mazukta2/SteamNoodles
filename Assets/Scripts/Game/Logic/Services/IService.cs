using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Services
{
    /*
     * Services are providing access to parts of the code that too far away from class purpuse logically.
     * For example localization, or logs, or controls
     */
    public interface IService : IDisposable
    {
    }
}
