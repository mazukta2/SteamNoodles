namespace Game.Assets.Scripts.Game.Logic.Presenters.Localization
{
    public class DefaultLocalizationService : ILocalizationManagerService
    {
        private static ILocalizationManager _service;

        public ILocalizationManager Get()
        {
            return _service;
        }

        public void Set(ILocalizationManager localization)
        {
            _service = localization;
        }

        public void Clear()
        {
            _service = null;
        }
    }
}
