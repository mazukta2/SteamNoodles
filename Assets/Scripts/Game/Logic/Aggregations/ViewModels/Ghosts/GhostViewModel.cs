using Game.Assets.Scripts.Game.Environment.Creation;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;

namespace Game.Assets.Scripts.Game.Logic.Aggregations.ViewModels.Ghosts
{
    public class GhostViewModel : Disposable
    {
        public Uid Id { get; private set; }
        public IViewPrefab Prefab { get; set; }
        public FieldRotation Rotation { get; set; }
        public GameVector3 WorldPosition { get; set; }
        public bool CanBuild { get; set; }

        public GhostViewModel(Uid id)
        {
            Id = id;
        }
    }
}