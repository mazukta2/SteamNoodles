using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Services;
using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Customers;
using Game.Assets.Scripts.Game.Logic.Services.Definitions;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Units;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Level.Units
{
    public class UnitModelPresenter : BasePresenter<IUnitModelView>
    {
        private IUnitModelView _view;
        private UnitsSettingsDefinition _settings;


        public UnitModelPresenter(IUnitModelView view) 
            : this(view, 
                  IPresenterServices.Default.Get<DefinitionsService>().Get<UnitsSettingsDefinition>())
        {

        }

        public UnitModelPresenter(IUnitModelView view, UnitsSettingsDefinition unitsSettingsDefinition) : base(view)
        {
            _view = view;
            _settings = unitsSettingsDefinition ?? throw new ArgumentNullException(nameof(unitsSettingsDefinition));

            DressUnit();
            PlayAnimation(Animations.Idle);
        }

        private void PlayAnimation(Animations animations)
        {
            _view.Animator.Play(animations.ToString());
        }

        private void DressUnit()
        {
            var random = new Random();
            var hair = _settings.Hairs.GetRandom(random);

            _view.UnitDresser.Clear();
            _view.UnitDresser.SetHair(hair);
        }

        public enum Animations
        {
            Idle,
            Run
        }
    }
}
