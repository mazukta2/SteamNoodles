using System;
using Game.Assets.Scripts.Game.Logic.Common.Core;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;
using Game.Assets.Scripts.Game.Logic.Models.Sequencer;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens.Widgets;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets
{
    public class DialogPresenter : Disposable, IPresenter
    {
        private IDialogView _view;
        private LevelSequencer _levelSequencer;
        private DialogStep _currentStep;

        public DialogPresenter(IDialogView view) : this(view, IModels.Default.Find<LevelSequencer>())
        {

        }

        public DialogPresenter(IDialogView view, LevelSequencer levelSequence)
        {
            _view = view;
            _view.Next.SetAction(Click);
            _levelSequencer = levelSequence;
            _levelSequencer.OnStepStarted += LevelSequence_OnStepStarted;
            _levelSequencer.OnStepFinished += LevelSequence_OnStepFinished;
        }

        protected override void DisposeInner()
        {
            _levelSequencer.OnStepStarted -= LevelSequence_OnStepStarted;
            _levelSequencer.OnStepFinished -= LevelSequence_OnStepFinished;
        }

        private void LevelSequence_OnStepStarted(Models.Cutscenes.LevelSequencerStep obj)
        {
            if (!(obj is DialogStep dialogStep))
                return;
            _currentStep = dialogStep;
            _view.Animator.Play("Show");
        }

        private void LevelSequence_OnStepFinished(Models.Cutscenes.LevelSequencerStep obj)
        {
            if (!(obj is DialogStep dialogStep))
                return;

            _currentStep = null;
            _view.Animator.Play("Hide");
        }

        private void Click()
        {
            _currentStep.Process();
        }
    }
}

