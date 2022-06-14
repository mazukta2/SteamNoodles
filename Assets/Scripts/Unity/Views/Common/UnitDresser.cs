using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace GameUnity.Assets.Scripts.Unity.Views.Common
{
    public class UnitDresser : IUnitDresser
    {
        private UnitRigs _rigs;
        private GameAssetsService _assets;

        public UnitDresser(UnitRigs rigs, GameAssetsService assets)
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
            var hairPrefab = _assets.GetPrefab(hair);
            _rigs.Head.Spawn(hairPrefab);
        }
    }
}
