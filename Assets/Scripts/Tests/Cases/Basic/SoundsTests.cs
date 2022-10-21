using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Music;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using Game.Assets.Scripts.Game.Logic.Views.Controls;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Environment.Game;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class SoundsTests
    {
        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        [Test, Order(TestOrders.Models)]
        public void MusicEmptyToSound()
        {
            var controls = new GameControls(new ControlsMock());
            var time = new GameTime();
            var manager = new MusicManager(controls, time);

            Assert.IsNull(manager.GetCurrent());
            Assert.IsNull(manager.GetTarget());

            manager.Start("track1");

            Assert.IsNotNull(manager.GetCurrent());
            Assert.IsNotNull(manager.GetTarget());
            Assert.AreEqual(manager.GetCurrent(), manager.GetTarget());
            Assert.AreEqual("track1", manager.GetCurrent().Name);
            Assert.AreEqual(1, manager.GetCurrent().Volume);

            controls.Dispose();
            manager.Dispose();
        }

        [Test, Order(TestOrders.Models)]
        public void MusicSoundToEmpty()
        {
            var controls = new GameControls(new ControlsMock());
            var time = new GameTime();
            var manager = new MusicManager(controls, time);
            manager.Start("track1");
            time.MoveTime(1);

            manager.Stop();

            Assert.IsNotNull(manager.GetCurrent());
            Assert.IsNull(manager.GetTarget());
            var track = manager.GetCurrent();
            Assert.IsFalse(track.IsDisposed);

            Assert.AreEqual(1, manager.GetCurrent().Volume);

            time.MoveTime(1f);
            Assert.IsFalse(track.IsDisposed);

            Assert.AreEqual(0.5f, manager.GetCurrent().Volume);

            time.MoveTime(1f);
            
            Assert.IsNull(manager.GetCurrent());
            Assert.IsNull(manager.GetTarget());
            Assert.IsTrue(track.IsDisposed);

            controls.Dispose();
            manager.Dispose();
        }
    }
}
