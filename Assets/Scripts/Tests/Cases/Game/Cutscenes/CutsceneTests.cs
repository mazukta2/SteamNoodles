using System;
using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Definitions.Levels;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Tests.Environment;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Cutscenes
{
    public class CutsceneTests
    {
        [Test, Order(TestOrders.First)]
        public void TimeStepWorks()
        {
            var time = new GameTime();

            var steps = new List<CutsceneStep>()
            {
                new WaitStep(3f, time),
                new WaitStep(3f, time)
            };

            var cutscene = new Cutscene(steps);

            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(1f);

            Assert.AreEqual(0, cutscene.GetProcessedStepsAmount());
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.AreEqual(1, cutscene.GetProcessedStepsAmount());
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.AreEqual(2, cutscene.GetProcessedStepsAmount());
            Assert.IsTrue(cutscene.IsDisposed);
        }

        [Test, Order(TestOrders.First)]
        public void SwitchCameraWorks()
        {
            var time = new GameTime();
            var gameControls = new ControlsMock();

            var steps = new List<CutsceneStep>()
            {
                new SwitchCameraStep(3f, "Camera1", time, gameControls),
            };

            var cutscene = new Cutscene(steps);

            Assert.AreEqual("Camera1", gameControls.CurrentCamera);
            Assert.IsFalse(cutscene.IsDisposed);

            time.MoveTime(3f);

            Assert.IsTrue(cutscene.IsDisposed);
        }
    }
}

