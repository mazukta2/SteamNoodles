using System;
using System.Xml.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Sequencer;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class DialogPresenter : Disposable, IPresenter
    {
        private IDialogView _view;
        private LevelSequencer _levelSequencer;
        private LocalizatedText _name;
        private LocalizatedText _text;

        public DialogPresenter(IDialogView view) : this(view, IModels.Default.Find<LevelSequencer>())
        {

        }

        public DialogPresenter(IDialogView view, LevelSequencer levelSequence)
        {
            _view = view;

            _view.Next.SetAction(Click);
            _levelSequencer = levelSequence;
            //_levelSequencer.OnStepStarted += LevelSequence_OnStepStarted;
            //_levelSequencer.OnStepFinished += LevelSequence_OnStepFinished;
        }

        protected override void DisposeInner()
        {
            //_levelSequencer.OnStepStarted -= LevelSequence_OnStepStarted;
            //_levelSequencer.OnStepFinished -= LevelSequence_OnStepFinished;
            _name?.Dispose();
            _text?.Dispose();
        }

        private void LevelSequence_OnStepStarted(Models.Cutscenes.LevelSequencerStep obj)
        {
            //if (!(obj is DialogStep dialogStep))
             //   return;
            //_currentStep = dialogStep;
            _view.Animator.Play("Show");

            _name?.Dispose();
            _text?.Dispose();
            //_name = new LocalizatedText(_view.Name, dialogStep.Name);
            //_text = new LocalizatedText(_view.Text, dialogStep.Text);
        }

        private void LevelSequence_OnStepFinished(Models.Cutscenes.LevelSequencerStep obj)
        {
            //if (!(obj is DialogStep dialogStep))
            //    return;

            //_currentStep = null;
            //_view.Animator.Play("Hide");
        }

        private void Click()
        {
            //_currentStep.Process();
        }
    }
}

