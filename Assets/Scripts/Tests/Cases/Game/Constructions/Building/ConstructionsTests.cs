using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Services.Commands;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Requests;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Commands.Constructions.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Presenters.Requests.Constructions;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Presenters.Commands;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class ConstructionsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsCellCalculationIsCorrect()
        {
            var calculator = new FieldService(10, new IntPoint(100, 100));
            var size = new IntRect(0, 0, 1, 1);
            Assert.AreEqual(new GameVector3(0, 0, 0), calculator.GetMapPositionByGridPosition(new IntPoint(0, 0), size));
            Assert.AreEqual(new GameVector3(10, 0, -10), calculator.GetMapPositionByGridPosition(new IntPoint(1, -1), size));
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsRectSizeCorrect()
        {
            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 1, 1 },
                    { 1, 1 }
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 1, 1, 0 },
                    { 1, 1, 0 }
                }

            );

            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 1, 1 },
                    { 1, 1 },
                    { 0, 0 }
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 0, 1, 1 },
                    { 0, 1, 1 },
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 0, 0 },
                    { 1, 1 },
                    { 1, 1 },
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new int[,] {
                    { 0, 0, 0, 0 },
                    { 0, 1, 1, 0 },
                    { 0, 1, 1, 0 },
                    { 0, 0, 0, 0 },
                }
            );

            TestRect(new IntRect(0, 0, 3, 3),
                new int[,] {
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                }
            );


            TestRect(new IntRect(0, 0, 3, 3),
                new int[,] {
                    { 0, 0, 0, 0 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                }
            );


            TestRect(new IntRect(0, 0, 3, 3),
                new int[,] {
                    { 0, 0, 0, 0, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                }
            );

            TestRect(new IntRect(0, 0, 3, 3),
                new int[,] {
                    { 0, 0, 0, 0, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 0, 0, 0, 0 },
                }
            );

            void TestRect(IntRect expected, int[,] rect)
            {
                var placement = new ContructionPlacement(rect);
                Assert.AreEqual(expected, placement.GetRect(new(FieldRotation.Rotation.Top)));
            }
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPlacementCreateCellsSize()
        {
            var events = new EventManager();
            var constructionsRepository = new Repository<Construction>(events);
            var buildinMode = new BuildingModeService(events);
            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var requests = new RequestsMock()
                .Add(new FieldRequestsService(fieldService, constructionService, buildinMode));

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view,
                requests.Get<GetField>(), new CommandsMock(), events);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsBuildingWorks()
        {
            var events = new EventManager();
            var constructionsRepository = new Repository<Construction>(events);
            var constructionsCardsRepository = new Repository<ConstructionCard>(events);
            var constructionsSchemeRepository = new Repository<ConstructionScheme>(events);

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository, constructionsSchemeRepository);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var buildngService = new BuildingService(constructionsRepository, constructionsService, pointsService, handService, fieldService);

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            constructionsCardsRepository.Add(card);

            Assert.AreEqual(0, constructionsRepository.Count);

            buildngService.Build(card, new FieldPosition(1, 1), new FieldRotation());

            Assert.AreEqual(1, constructionsRepository.Count);
            var construction = constructionsRepository.GetAll().First();
            Assert.AreEqual(new FieldPosition(1, 1), construction.Position);
            Assert.AreEqual(scheme, construction.Scheme);

            Assert.AreEqual(0, constructionsCardsRepository.Count);
            
            pointsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionPresenterSpawning()
        {
            var events = new EventManager();
            var constructionsRepository = new Repository<Construction>(events);
            var buildinMode = new BuildingModeService(events);
            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);

            var command = new CommandsMock();
            var requests = new RequestsMock()
                .Add(new FieldRequestsService(fieldService, constructionService, buildinMode));

            new PlacementFieldPresenter(view, 
                requests.Get<GetField>(), command, events);

            Assert.IsTrue(command.IsEmpty());
            constructionsRepository.Add(new Construction(new ConstructionScheme(), new FieldPosition(1, 1), new FieldRotation()));

            Assert.IsTrue(command.Last<BuildConstructionCommand>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsOffsetRight()
        {
            {
                var fieldPoisition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 1, 1);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new IntPoint(0, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new IntPoint(1, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new IntPoint(2, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPoisition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 2, 2);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new IntPoint(0, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new IntPoint(1, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new IntPoint(2, 0), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0), size));
            }

            {
                var fieldPoisition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 3, 3);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new IntPoint(-1, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new IntPoint(0, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new IntPoint(1, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPoisition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPoisition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 4, 4);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new IntPoint(-1, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0.1f), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new IntPoint(0, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0.1f), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new IntPoint(1, -1), fieldPoisition.GetGridPositionByMapPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPoisition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0.1f), size));
            }
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            var events = new EventManager();
            var constructionsRepository = new Repository<Construction>(events);
            var constructionsCardsRepository = new Repository<ConstructionCard>(events);
            var constructionsSchemeRepository = new Repository<ConstructionScheme>(events);

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
 
            var placement = new ContructionPlacement(new int[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());
            constructionsSchemeRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            constructionsCardsRepository.Add(card);

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var commands = new CommandsMock();

            var constructionView = new ConstructionView(viewCollection);
            new ConstructionPresenter(constructionView, construction, fieldService, commands);

            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), constructionView.Position.Value);

            pointsService.Dispose();
            viewCollection.Dispose();
        }

        [Test]
        public void IsConstructionPlacedHaveRightVisual()
        {
            var events = new EventManager();
            var constructionsRepository = new Repository<Construction>(events);
            var constructionsCardsRepository = new Repository<ConstructionCard>(events);
            var constructionsSchemeRepository = new Repository<ConstructionScheme>(events);

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var fieldService = new FieldService(1, new IntPoint(11, 11));

            var placement = new ContructionPlacement(new int[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses(),
                "", "", new Requirements());
            constructionsSchemeRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            constructionsCardsRepository.Add(card);

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var commands = new CommandsMock();

            var constructionView = new ConstructionView(viewCollection);
            new ConstructionPresenter(constructionView, construction, fieldService, commands);

            Assert.IsTrue(commands.Last<AddConstructionModelCommand>());

            pointsService.Dispose();
            viewCollection.Dispose();
        }



        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

    }
}
