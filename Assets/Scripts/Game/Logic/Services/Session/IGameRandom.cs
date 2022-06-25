namespace Game.Assets.Scripts.Game.Logic.Services.Session
{
    public interface IGameRandom
    {
        static IGameRandom Default { get; set; }
        public int GetRandom(int minValue, int maxValue);
        public float GetRandom(float minValue, float maxValue);
        public bool GetRandom();
    }
}
