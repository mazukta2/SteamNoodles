using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;
using Game.Assets.Scripts.Game.Logic.Models.Sequencer;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Cutscenes
{
    public class CutsceneTests
    {
        [Test, Order(TestOrders.Models)]
        public void TimeStepWorks()
        {
            var time = new GameTime();

            var steps = new List<CutsceneStep>()
            {
                new WaitStep(3f, time),
                new WaitStep(3f, time)
            };

            var levelSequencer = new LevelSequencer();
            var cutscene = new Cutscene(levelSequencer, steps);

            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(1f);

            Assert.AreEqual(0, levelSequencer.GetProcessedStepsAmount());
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.AreEqual(1, levelSequencer.GetProcessedStepsAmount());
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.AreEqual(2, levelSequencer.GetProcessedStepsAmount());
            Assert.IsTrue(cutscene.IsDisposed);
            levelSequencer.Dispose();
        }

        [Test, Order(TestOrders.Models)]
        public void SwitchCameraWorks()
        {
            var time = new GameTime();
            var gameControls = new ControlsMock();

            var steps = new List<CutsceneStep>()
            {
                new SwitchCameraStep(3f, "Camera1", time, gameControls),
            };

            var levelSequencer = new LevelSequencer();
            var cutscene = new Cutscene(levelSequencer, steps);

            Assert.AreEqual("Camera1", gameControls.CurrentCamera);
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.IsTrue(cutscene.IsDisposed);
            levelSequencer.Dispose();
        }

        [Test, Order(TestOrders.Models)]
        public void DialogsWork()
        {
            var steps = new List<CutsceneStep>()
            {
                new DialogStep(),
            };

            var levelSequencer = new LevelSequencer();
            var cutscene = new Cutscene(levelSequencer, steps);

            Assert.IsFalse(cutscene.IsDisposed);

            Assert.IsTrue(levelSequencer.GetCurrentStep() is DialogStep);

            Assert.IsFalse(cutscene.IsDisposed);

            ((DialogStep)levelSequencer.GetCurrentStep()).Process();

            Assert.IsTrue(cutscene.IsDisposed);

            levelSequencer.Dispose();
        }

        [Test, Order(TestOrders.Presenters)]
        public void DialogsVisualWorks()
        {
            var views = new DefaultViews();
            var levelSequencer = new LevelSequencer();
            var view = new DialogView(views);
            var presenter = new DialogPresenter(view, levelSequencer);

            Assert.AreEqual(null, view.Animator.Animation);

            var steps = new List<CutsceneStep>()
            {
                new DialogStep(),
            };

            var cutscene = new Cutscene(levelSequencer, steps);

            Assert.AreEqual("Show", view.Animator.Animation);

            view.Button.Click();

            Assert.IsTrue(cutscene.IsDisposed);

            levelSequencer.Dispose();

            presenter.Dispose();
            views.Dispose();
        }
    }
}

