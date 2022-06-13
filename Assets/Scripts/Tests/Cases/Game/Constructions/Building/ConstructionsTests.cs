using Game.Assets.Scripts.Game.Logic.Common.Math;
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
using Game.Assets.Scripts.Game.Logic.Presenters.Constructions.Placements;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Assets;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Models.Constructions;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Tests.Cases;
using NUnit.Framework;
using System.Collections.Generic;
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
        public void IsPlacementBoundariesRequestRight()
        {
            var constructionsRepository = new Repository<Construction>();
            var buildinMode = new BuildingModeService();
            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            
            var service = new FieldRequestsService(fieldService, constructionService, buildinMode);
            var model = service.Get();

            Assert.AreEqual(-1, model.Boudaries.Value.xMin);
            Assert.AreEqual(-1, model.Boudaries.Value.yMin);
            Assert.AreEqual(1, model.Boudaries.Value.xMax);
            Assert.AreEqual(1, model.Boudaries.Value.yMax);

            Assert.AreEqual(new GameVector3(-10, 0, -10), model.GetCellWorldPosition(new IntPoint(-1, -1)));
            Assert.AreEqual(new GameVector3(-10, 0, 10), model.GetCellWorldPosition(new IntPoint(-1, 1)));
            Assert.AreEqual(new GameVector3(10, 0, 10), model.GetCellWorldPosition(new IntPoint(1, 1)));
            Assert.AreEqual(new GameVector3(10, 0, -10), model.GetCellWorldPosition(new IntPoint(1, -1)));

            model.Dispose();
            service.Dispose();
            constructionService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsPlacementCreateCellsSize()
        {
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);

            var positions = new Dictionary<IntPoint, GameVector3>();
            var status = new Dictionary<IntPoint, CellPlacementStatus>();
            for (int x = -1; x <= 1; x++)
                for (int y = -1; y <= 1; y++)
                {
                    positions.Add(new IntPoint(x, y), new GameVector3(x * 10, 0, y * 10));
                    status.Add(new IntPoint(x, y), CellPlacementStatus.Normal);
                }

            var model = new FieldModelMock(status, positions, new FieldBoundaries(new(3, 3)));
            new PlacementFieldPresenter(view, model);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));

            viewCollection.Dispose();
            model.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsBuildingWorks()
        {
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();

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
            constructionsService.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionPresenterSpawning()
        {
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);

            var fieldModel = new FieldModelMock();

            new PlacementFieldPresenter(view, fieldModel);

            fieldModel.FireOnConstructionBuilded(new Uid());

            Assert.IsTrue(view.ConstrcutionContainer.Has<IConstructionView>());

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

        [Test, Order(TestCore.ModelOrder)]
        public void ShinkIsCorrect()
        {
            var controls = new GameControls(new ControlsMock());
            var gameAssets = new GameAssets(new AssetsMock());
            var constructionsRepository = new Repository<Construction>();

            var buildingMode = new BuildingModeService();
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var scheme = new ConstructionScheme(ghostHalfShrinkDistance: 1, ghostShrinkDistance : 4);

            var construction = new Construction(scheme, new FieldPosition(0, 0), new FieldRotation());
            constructionsRepository.Add(construction);

            var requests = new ConstructionsRequestsService(constructionsRepository, buildingMode, 
                fieldService, gameAssets, controls);
            var model = requests.Get(construction.Id);
            var update = 0;
            model.OnUpdate += HandleUpdate;
            Assert.AreEqual(1, model.GetShrinkValue());

            buildingMode.Show(new ConstructionCard(scheme));

            Assert.AreEqual(1, update);
            Assert.AreEqual(0.2f, model.GetShrinkValue());

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 1));

            Assert.AreEqual(2, update);
            Assert.AreEqual(0.2f, model.GetShrinkValue());

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 2));

            Assert.AreEqual(3, update);
            Assert.AreEqual(0.5f, model.GetShrinkValue());

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 4));

            Assert.AreEqual(4, update);
            Assert.AreEqual(1f, model.GetShrinkValue());

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 0));
            Assert.AreEqual(5, update);
            Assert.AreEqual(0.2f, model.GetShrinkValue());

            buildingMode.Hide();

            Assert.AreEqual(6, update);
            Assert.AreEqual(1f, model.GetShrinkValue());

            model.OnUpdate -= HandleUpdate;

            model.Dispose();
            requests.Dispose();
            controls.Dispose();

            void HandleUpdate()
            {
                update++;
            }
        }

        [Test, Order(TestCore.ModelOrder)]
        public void ExplosionWorks()
        {
            var controls = new GameControls(new ControlsMock());
            var gameAssets = new GameAssets(new AssetsMock());
            var constructionsRepository = new Repository<Construction>();

            var buildingMode = new BuildingModeService();
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var scheme = new ConstructionScheme(ghostHalfShrinkDistance: 1, ghostShrinkDistance: 4);

            var construction = new Construction(scheme, new FieldPosition(0, 0), new FieldRotation());
            constructionsRepository.Add(construction);

            var requests = new ConstructionsRequestsService(constructionsRepository, buildingMode,
                fieldService, gameAssets, controls);
            var model = requests.Get(construction.Id);
            var explosion = 0;
            model.OnExplostion += HandleExplosion;

            constructionsRepository.Remove(construction);
            Assert.AreEqual(1, explosion);

            model.OnExplostion -= HandleExplosion;

            model.Dispose();
            requests.Dispose();
            controls.Dispose();

            void HandleExplosion()
            {
                explosion++;
            }
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsConstructionRequestCorrect()
        {
            var controls = new GameControls(new ControlsMock());
            var gameAssets = new GameAssets(new AssetsMock());
            var constructionsRepository = new Repository<Construction>();

            var buildingMode = new BuildingModeService();
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

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var requests = new ConstructionsRequestsService(constructionsRepository, buildingMode, 
                fieldService, gameAssets, controls);
            var model = requests.Get(construction.Id);
            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), model.GetWorldPosition());
            model.Dispose();
            requests.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionPlacedInRightPosition()
        {
            var viewCollection = new ViewsCollection();

            var constructionView = new ConstructionView(viewCollection);
            var model = new ConstructionModelMock(new FieldRotation(), new GameVector3(1.5f, 0, 1f));
            new ConstructionPresenter(constructionView, model);

            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), constructionView.Position.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionSpawnsVisual()
        {
            var viewCollection = new ViewsCollection();
            var model = new ConstructionModelMock(FieldRotation.Default, GameVector3.Zero);

            var constructionView = new ConstructionView(viewCollection);

            Assert.IsNull(constructionView.Container.FindView<IConstructionModelView>());

            new ConstructionPresenter(constructionView, model);

            Assert.IsNotNull(constructionView.Container.FindView<IConstructionModelView>());

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ShinkPresenting()
        {
            var viewCollection = new ViewsCollection();
            var model = new ConstructionModelMock(FieldRotation.Default, GameVector3.Zero);
            model.SetShrink(0.5f);

            var constructionView = new ConstructionView(viewCollection);
            new ConstructionPresenter(constructionView, model);
            var modelView = constructionView.Container.FindView<IConstructionModelView>();

            Assert.IsNotNull(modelView);

            Assert.AreEqual(0.5f, modelView.Shrink.Value);

            model.SetShrink(0.1f);

            Assert.AreEqual(0.1f, modelView.Shrink.Value);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void DropAnimations()
        {
            var viewCollection = new ViewsCollection();
            var model = new ConstructionModelMock();

            var constructionView = new ConstructionView(viewCollection);

            new ConstructionPresenter(constructionView, model);
            var modelView = constructionView.Container.FindView<IConstructionModelView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            viewCollection.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ExpolodeAnimations()
        {
            var viewCollection = new ViewsCollection();
            var model = new ConstructionModelMock();

            var constructionView = new ConstructionView(viewCollection);

            new ConstructionPresenter(constructionView, model);
            var modelView = constructionView.Container.FindView<IConstructionModelView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            Assert.IsFalse(constructionView.IsDisposed);

            model.FireExplode();

            Assert.AreEqual("Explode", animator.Animations[2]);
            Assert.AreEqual("Idle", animator.Animations[3]);

            Assert.IsTrue(constructionView.IsDisposed);
            
            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

    }
}
