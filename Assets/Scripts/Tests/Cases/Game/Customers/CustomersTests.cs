namespace Game.Tests.Cases.Customers
{
    public class CustomersTests
    {
        //[Test]
        //public void IsCurrentCustomerSetted()
        //{
        //    var game = new GameController();
        //    var levelProto = new LevelSettings();

        //    var (models, _, views) = game.LoadLevel(levelProto);
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));
        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.IsTrue(models.Clashes.IsInClash);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count);

        //    game.Exit();
        //}

        //[Test]
        //public void IsCurrentConsumerChanged()
        //{
        //    var game = new GameController();
        //    var levelProto = new LevelSettings();

        //    var (models, _, views) = game.LoadLevel(levelProto);
        //    var customerSettings = (CustomerSettings)models.Units.GetPool().First();
        //    customerSettings.Speed = 1000;
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));
        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.IsTrue(models.Clashes.IsInClash);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    var consumer = models.Clashes.CurrentClash.Customers.GetCustomers().First();
        //    Assert.IsNotNull(consumer);

        //    Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.Ordering, models.Clashes.CurrentClash.Customers.GetCustomers()[0].CurrentPhase);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.InQueue, models.Clashes.CurrentClash.Customers.GetCustomers()[1].CurrentPhase);

        //    CommonTestActions.ServeCustumer(game, models);
        //    Assert.IsTrue(consumer.Unit.IsServed);
        //    Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count());
        //    Assert.AreEqual(ServingCustomerProcess.Phase.Exiting, consumer.CurrentPhase);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.Ordering, models.Clashes.CurrentClash.Customers.GetCustomers().First().CurrentPhase);

        //    game.Exit();
        //}

        //[Test]
        //public void IsCustumerPoolGiveRightCustumers()
        //{
        //    var game = new GameController();
            
        //    var (models, presenters, views) = game.LoadLevel();
        //    var customer1 = (CustomerSettings)models.Units.GetPool().First();
        //    var customer2 = new CustomerSettings();
        //    var settings = (LevelSettings)models.Clashes.Settings;
        //    settings.SpawnQueueTime = 1;
        //    settings.MaxQueue = 3;
        //    models.Units.AddPotentialCustumer(customer2);
        //    models.Units.AddPotentialCustumer(customer2);
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));

        //    views.Screen.Clashes.StartClash.Click();
        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    Assert.AreEqual(3, models.Clashes.CurrentClash.Customers.GetCustomers().Count());

        //    Assert.AreEqual(1, models.Clashes.CurrentClash.Customers.GetCustomers().Count(x => x.Unit.Settings == customer1));
        //    Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Count(x => x.Unit.Settings == customer2));

        //    game.Exit();
        //}

        //[Test]
        //public void IsCustomersGiveYourMoney()
        //{
        //    var game = new GameController();

        //    var (models, _, views) = game.LoadLevel();
        //    var customer1 = (CustomerSettings)models.Units.GetPool().First();
        //    customer1.Money = 3;
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));
        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.AreEqual(0, models.Money);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    var customer = models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit;
        //    CommonTestActions.ServeCustumer(game, models);
        //    Assert.IsTrue(customer.IsServed);
        //    Assert.AreEqual(3, models.Money);

        //    game.Exit();
        //}

        //[Test]
        //public void IsCostumerGivesTips()
        //{
        //    var game = new GameController();

        //    var (models, _, views) = game.LoadLevel();
        //    var unitSettings = (CustomerSettings)models.Units.GetPool().First();
        //    var constructionSettings = (ConstructionSettings)models.Hand.Cards.First().Settings;
        //    unitSettings.BaseTipMultiplayer = 2;
        //    constructionSettings.TagsList.Add(Assets.Scripts.Game.Logic.Models.Buildings.ConstructionTag.Service, 101);

        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));

        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.AreEqual(0, models.Money);
        //    Assert.AreEqual(101, models.Service);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    Assert.AreEqual(2, models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit.GetTips());

        //    CommonTestActions.ServeCustumer(game, models);

        //    Assert.AreEqual(3, models.Money); // 1 for normal, 1*2 = tips;

        //    game.Exit();
        //}

        //[Test]
        //public void IsCostumerServedRightTime()
        //{
        //    var game = new GameController();

        //    var (models, _, views) = game.LoadLevel();
        //    var customer1 = (CustomerSettings)models.Units.GetPool().First();
        //    customer1.OrderingTime = 2;
        //    customer1.CookingTime = 2;
        //    customer1.EatingTime = 2;
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));
        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.AreEqual(0, models.Money);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    var customer = models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit;
        //    Assert.IsTrue(customer.IsMoving());
        //    customer.TeleportToTarget();

        //    for (int i = 0; i < 6; i++)
        //    {
        //        Assert.IsFalse(customer.IsServed);
        //        Assert.IsFalse(customer.IsMoving());
        //        game.PushTime(1);
        //    }
        //    Assert.IsTrue(customer.IsMoving());
        //    customer.TeleportToTarget();
        //    Assert.IsTrue(customer.IsServed);

        //    game.Exit();
        //}

        //[Test]
        //public void IsConsumerPassAllPhases()
        //{
        //    var game = new GameController();

        //    var (models, _, views) = game.LoadLevel();
        //    var customer1 = (CustomerSettings)models.Units.GetPool().First();
        //    customer1.OrderingTime = 2;
        //    customer1.CookingTime = 2;
        //    customer1.EatingTime = 2;
        //    views.Screen.Hand.Cards.List.First().Button.Click();
        //    views.Placement.Click(new FloatPoint(0, 0));
        //    views.Screen.Clashes.StartClash.Click();

        //    Assert.AreEqual(0, models.Money);

        //    models.Clashes.CurrentClash.Customers.Queue.Add();
        //    var service = models.Clashes.CurrentClash.Customers.GetCustomers().Last();
        //    var customer = models.Clashes.CurrentClash.Customers.GetCustomers().Last().Unit;
        //    Assert.IsTrue(customer.IsMoving());
        //    customer.TeleportToTarget();

        //    Assert.AreEqual(ServingCustomerProcess.Phase.Ordering, service.CurrentPhase);
        //    game.PushTime(2);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.WaitCooking, service.CurrentPhase);
        //    game.PushTime(2);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.Eating, service.CurrentPhase);
        //    game.PushTime(2);
        //    Assert.AreEqual(ServingCustomerProcess.Phase.MovingAway, service.CurrentPhase);

        //    Assert.IsTrue(customer.IsMoving());
        //    customer.TeleportToTarget();
        //    Assert.IsTrue(customer.IsServed);

        //    game.Exit();
        //}

        //[TearDown]
        //public void TestDisposables()
        //{
        //    DisposeTests.TestDisposables();
        //}
    }
}
