using System;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class DialogStep : CutsceneStep
    {
        public ILocalizatedString Name { get; private set; }
        public ILocalizatedString Text { get; private set; }

        public DialogStep(Dialog definition) : this(new LocalizatedString(definition.Name), new LocalizatedString(definition.Text))
        {

        }

        public DialogStep(ILocalizatedString name, ILocalizatedString text)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        protected override void DisposeInner()
        {
        }

        public override void Play()
        {
        }

        public void Process()
        {
            FireOnFinished();
        }
    }
}

