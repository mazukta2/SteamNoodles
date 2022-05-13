namespace Game.Assets.Scripts.Game.Logic.Models.Units
{
    public interface ICrowd
    {
        void SendToCrowd(Unit unit, LevelCrowd.CrowdDirection direction);
    }
}
