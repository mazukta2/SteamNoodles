using Game.Assets.Scripts.Game.Logic.Repositories;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Entities;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;

namespace Game.Assets.Scripts.Tests.Cases.Basic
{
    public class RepositoryTests
    {
        [Test, Order(TestCore.EssentialOrder)]
        public void IsCopyWorking()
        {
            var entity1 = new TestEntity(1, new TestValueObject(2));
            var entity2 = (TestEntity)entity1.Copy();

            Assert.AreEqual(entity1.Value1, entity2.Value1);
            Assert.AreEqual(entity1.Value2, entity2.Value2);
            Assert.AreEqual(entity1.Value2.Value, entity2.Value2.Value);
            Assert.AreEqual(1, entity2.Value1);
            Assert.AreEqual(2, entity2.Value2.Value);
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void IsEntityEquilityWorking()
        {
            var entity1 = new TestEntity(new Uid(1), 1, new TestValueObject(2));
            var entity2 = new TestEntity(new Uid(1), 2, new TestValueObject(4));
            var entity3 = new TestEntity(new Uid(2), 1, new TestValueObject(2));
            var entity4 = new TestEntity(new Uid(1), 1, new TestValueObject(2));

            Assert.AreNotEqual(entity1, entity2);
            Assert.AreNotEqual(entity1, entity3);
            Assert.AreEqual(entity1, entity4);
            Assert.AreNotEqual(entity2, entity3);
            Assert.AreNotEqual(entity2, entity4);
            Assert.AreNotEqual(entity3, entity4);
        }

        [Test, Order(TestCore.EssentialOrder)]
        public void IsEventsWorking()
        {
            var entity = new TestEntity(1, new TestValueObject(2));
            var repository = new Repository<TestEntity>();
            repository.Add(entity);
            var isEvent = false;
            repository.OnEvent += Repository_OnModelEvent;
            
            Assert.IsFalse(isEvent);
            entity.SendEvent();
            repository.OnEvent -= Repository_OnModelEvent;

            Assert.IsTrue(isEvent);

            void Repository_OnModelEvent(TestEntity arg1, IModelEvent arg2)
            {
                isEvent = true;
            }
        }


        public sealed record TestEntity : Entity
        {
            public TestEntity(Uid id, int value1, TestValueObject value2) : base(id)
            {
                Value1 = value1;
                Value2 = value2;
            }

            public TestEntity(int value1, TestValueObject value2)
            {
                Value1 = value1;
                Value2 = value2;
            }

            public int Value1 { get; set; }
            public TestValueObject Value2 { get; set; }

            public void SendEvent()
            {
                FireEvent(new TestEvent());
            }
        }

        public record TestValueObject
        {
            public TestValueObject(int value)
            {
                Value = value;
            }

            public int Value { get; }
        }

        public record TestEvent : IModelEvent
        {

        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
