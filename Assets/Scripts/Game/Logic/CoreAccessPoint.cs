using Game.Assets.Scripts.Game.Environment;
using Game.Assets.Scripts.Game.Logic.Models;
using System;

namespace Game.Assets.Scripts.Game.Logic
{
    public static class CoreAccessPoint
    {
        public static Core Core { get; private set; }

        public static void SetCore(Core core)
        {
            if (Core != null)
                throw new Exception("Core already setted");

            Core = core;
        }

        public static void ClearCore()
        {
            if (Core == null)
                throw new Exception("Core not setted");

            Core = null;
        }
    }
}
