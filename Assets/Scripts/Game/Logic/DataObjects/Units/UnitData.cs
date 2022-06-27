using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Entities.Units;

namespace Game.Assets.Scripts.Game.Logic.DataObjects.Units
{
    public struct UnitData : IData
    {
        public UnitType UnitType { get; set; }
        public GameVector3 Position { get; set; }
        public GameVector3 Target { get; set; }
        public bool IsMoving { get; set; }
        public float CurrentSpeed { get; set; }
        public float MaxSpeed { get; set; }
        public string Hair { get; set; }
    }
}