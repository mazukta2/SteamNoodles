using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Views.Level.Units;
using GameUnity.Assets.Scripts.Unity.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class UnitDresser : IUnitDresser
    {
        private UnitRigs _rigs;
        private IAssets _assets;

        public UnitDresser(UnitRigs rigs, IAssets assets)
        {
            _rigs = rigs;
            _assets = assets;
        }

        public void Clear()
        {
            _rigs.Head.Clear();
        }

        public void SetHair(string hair)
        {
            //var hairPrefab =_assets.GetPrefab(hair);
            //_rigs.Head.Spawn(hairPrefab);
        }
    }
}
