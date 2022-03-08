using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Common;
using Game.Assets.Scripts.Game.Logic.Views.Sources;
using Game.Assets.Scripts.Game.Unity.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand
{
    public class HandView : View
    {
        public LevelSource Level;
        public ViewContainer Cards;
        public ViewPrototype CardPrototype;

        private HandPresenter _presenter;
        protected override void CreatedInner()
        {
            if (Level.GetLevel() == null)
                Level.OnChanged += Init;
            else
                Init(Level.GetLevel());
        }

        protected override void DisposeInner()
        {
            Level.OnChanged -= Init;
        }

        void Init(GameLevel level)
        {
            Level.OnChanged -= Init;
            _presenter = new HandPresenter(level.Hand, this);
        }

        public void OnValidate()
        {
            if (Level == null) throw new ArgumentNullException(nameof(Level));
            if (Cards == null) throw new ArgumentNullException(nameof(Cards));
            if (CardPrototype == null) throw new ArgumentNullException(nameof(CardPrototype));
        }
    }
}
