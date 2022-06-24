using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Services.Common;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Flow;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Ui.Screens.Widgets;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Ui.Screens.Widgets;
using NUnit.Framework;

namespace Game.Assets.Scripts.Tests.Cases.Game.Flow
{
    public class WavesTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void EndWaveWorksWhenHandIsEmpty()
        {
            var time = new GameTime();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var constructionDeck = new DeckService<ConstructionScheme>();

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var schemes = new SchemesService(constructionsSchemeRepository, constructionDeck);
            var stageLevel = new StageLevel(new[] { scheme });

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var rewardService = new RewardsService(stageLevel, handService, schemes, points);

            var waves = new StageWaveService(constructionsRepository, handService, rewardService, time);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(new Field(), 0, 0), new FieldRotation()));

            Assert.IsTrue(waves.CanFailWave());
            Assert.IsFalse(waves.CanWinWave());

            handService.Add(scheme);

            Assert.IsFalse(waves.CanFailWave());
            Assert.IsFalse(waves.CanWinWave());

            rewardService.Dispose();
            points.Dispose();
            waves.Dispose();
        }


        [Test, Order(TestCore.ModelOrder)]
        public void EndWaveButtonRemovesBuildings()
        {
            var time = new GameTime();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var constructionDeck = new DeckService<ConstructionScheme>();

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var field = new Field();
            var schemes = new SchemesService(constructionsSchemeRepository, constructionDeck,
                new Dictionary<ConstructionScheme, int>() { { scheme, 1 } });
            var stageLevel = new StageLevel(new[] { scheme });

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var rewardService = new RewardsService(stageLevel, handService, schemes, points);

            var waves = new StageWaveService(constructionsRepository, handService, rewardService, time, constructionsToEndWave: 4);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 0, 0), new FieldRotation()));
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 1, 0), new FieldRotation()));
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 2, 0), new FieldRotation()));

            Assert.AreEqual(3, constructionsRepository.Count);

            waves.FailWave();

            Assert.AreEqual(1, constructionsRepository.Count);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 1, 0), new FieldRotation()));
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 2, 0), new FieldRotation()));
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 2, 0), new FieldRotation()));

            Assert.AreEqual(4, constructionsRepository.Count);

            waves.WinWave();

            Assert.AreEqual(1, constructionsRepository.Count);

            rewardService.Dispose();
            points.Dispose();
            waves.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void WaveProgressWorks()
        {
            var time = new GameTime();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var constructionDeck = new DeckService<ConstructionScheme>();

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var schemes = new SchemesService(constructionsSchemeRepository, constructionDeck,
                new Dictionary<ConstructionScheme, int>() { { scheme, 1 } });
            var stageLevel = new StageLevel(new[] { scheme });

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var rewardService = new RewardsService(stageLevel, handService, schemes, points);

            var field = new Field();
            var waves = new StageWaveService(constructionsRepository, handService, rewardService, time, constructionsToEndWave: 2);

            Assert.AreEqual(0, waves.GetWaveProgress());
            Assert.IsFalse(waves.CanWinWave());

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 0, 0), new FieldRotation()));

            Assert.AreEqual(0.5f, waves.GetWaveProgress());
            Assert.IsFalse(waves.CanWinWave());

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 1, 0), new FieldRotation()));

            Assert.AreEqual(1f, waves.GetWaveProgress());
            Assert.IsTrue(waves.CanWinWave());

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field, 3, 0), new FieldRotation()));

            Assert.AreEqual(1f, waves.GetWaveProgress());
            Assert.IsTrue(waves.CanWinWave());

            rewardService.Dispose();
            points.Dispose();
            waves.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void EndWaveGiveYouNewBuildings()
        {
            var time = new GameTime();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var constructionDeck = new DeckService<ConstructionScheme>();

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var schemes = new SchemesService(constructionsSchemeRepository, constructionDeck,
                new Dictionary<ConstructionScheme, int>() { { scheme, 1 } });
            var stageLevel = new StageLevel(new[] { scheme });

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var rewardService = new RewardsService(stageLevel, handService, schemes, points);
            var field = new Field();

            var waves = new StageWaveService(constructionsRepository, handService, rewardService, time, constructionsToEndWave: 2);
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field,0, 0), new FieldRotation()));

            Assert.AreEqual(0, constructionsCardsRepository.Count);

            waves.FailWave();

            Assert.AreEqual(1, constructionsCardsRepository.Count);
            Assert.AreEqual(new CardAmount(3), constructionsCardsRepository.Get().First().Amount);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field,1, 0), new FieldRotation()));
            waves.WinWave();
            
            Assert.AreEqual(1, constructionsCardsRepository.Count);
            Assert.AreEqual(new CardAmount(6), constructionsCardsRepository.Get().First().Amount);

            rewardService.Dispose();
            points.Dispose();
            waves.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ButtonsAreActivated()
        {
            var time = new GameTime();
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();
            var constructionDeck = new DeckService<ConstructionScheme>();

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var schemes = new SchemesService(constructionsSchemeRepository, constructionDeck,
                new Dictionary<ConstructionScheme, int>() { { scheme, 1 } });
            var stageLevel = new StageLevel(new[] { scheme });

            var points = new BuildingPointsService(0, 0, time, 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var rewardService = new RewardsService(stageLevel, handService, schemes, points);

            var waves = new StageWaveService(constructionsRepository, handService, rewardService, time, constructionsToEndWave: 3);

            var levelCollection = new ViewsCollection();

            var view = new EndWaveButtonView(levelCollection);
            new EndWaveButtonWidgetPresenter(view, constructionsRepository, waves);

            constructionsCardsRepository.Add(new ConstructionCard(scheme));
            var field = new Field();

            Assert.IsFalse(view.NextWaveButton.IsActive);
            Assert.IsFalse(view.FailWaveButton.IsActive);
            Assert.AreEqual(EndWaveButtonWidgetPresenter.WaveButtonAnimations.None.ToString(),
                view.WaveButtonAnimator.Animation);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field,0, 0), new FieldRotation()));

            Assert.IsFalse(view.NextWaveButton.IsActive);
            Assert.IsFalse(view.FailWaveButton.IsActive);
            Assert.AreEqual(EndWaveButtonWidgetPresenter.WaveButtonAnimations.NextWave.ToString(),
                view.WaveButtonAnimator.Animation);

            constructionsCardsRepository.Clear();
            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field,1, 0), new FieldRotation()));

            Assert.IsFalse(view.NextWaveButton.IsActive);
            Assert.IsTrue(view.FailWaveButton.IsActive);
            Assert.AreEqual(EndWaveButtonWidgetPresenter.WaveButtonAnimations.FailWave.ToString(),
                view.WaveButtonAnimator.Animation);

            levelCollection.Dispose();
            rewardService.Dispose();
            points.Dispose();
            waves.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
