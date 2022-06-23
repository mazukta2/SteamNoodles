using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Presenters.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class ConstructionsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsCellCalculationIsCorrect()
        {
            var calculator = new FieldService(10, new IntPoint(100, 100));
            var size = new IntRect(0, 0, 1, 1);
            Assert.AreEqual(new GameVector3(0, 0, 0), calculator.GetWorldPosition(new FieldPosition(0, 0), size));
            Assert.AreEqual(new GameVector3(10, 0, -10), calculator.GetWorldPosition(new FieldPosition(1, -1), size));
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsRectSizeCorrect()
        {
            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 1, 1 },
                    { 1, 1 }
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 1, 1, 0 },
                    { 1, 1, 0 }
                }

            );

            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 1, 1 },
                    { 1, 1 },
                    { 0, 0 }
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 0, 1, 1 },
                    { 0, 1, 1 },
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 0, 0 },
                    { 1, 1 },
                    { 1, 1 },
                }
            );

            TestRect(new IntRect(0, 0, 2, 2),
                new[,] {
                    { 0, 0, 0, 0 },
                    { 0, 1, 1, 0 },
                    { 0, 1, 1, 0 },
                    { 0, 0, 0, 0 },
                }
            );

            TestRect(new IntRect(0, 0, 3, 3),
                new[,] {
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 0, 0, 0 },
                }
            );


            TestRect(new IntRect(0, 0, 3, 3),
                new[,] {
                    { 0, 0, 0, 0 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                    { 0, 1, 1, 1 },
                }
            );


            TestRect(new IntRect(0, 0, 3, 3),
                new[,] {
                    { 0, 0, 0, 0, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                    { 0, 1, 1, 1, 0 },
                }
            );

            TestRect(new IntRect(0, 0, 3, 3),
                new[,] {
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
            var fieldService = new FieldService(10, new IntPoint(3, 3));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, fieldService,controls);
            var constructionService = new ConstructionsService(constructionsRepository, fieldService);
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);

            new PlacementFieldPresenter(view, ghostService, fieldService, constructionService);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            viewCollection.Dispose();

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));

            constructionService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsBuildingWorks()
        {
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();
            var constructionsSchemeRepository = new Repository<ConstructionScheme>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var buildingService = new BuildingService(constructionsRepository, constructionsService, pointsService, handService, fieldService);

            var scheme = new ConstructionScheme();
            constructionsSchemeRepository.Add(scheme);

            var card = new ConstructionCard(scheme);
            constructionsCardsRepository.Add(card);

            Assert.AreEqual(0, constructionsRepository.Count);

            buildingService.Build(card, new FieldPosition(1, 1), new FieldRotation());

            Assert.AreEqual(1, constructionsRepository.Count);
            var construction = constructionsRepository.Get().First();
            Assert.AreEqual(new FieldPosition(1, 1), construction.Position);
            Assert.AreEqual(scheme, construction.Scheme);

            Assert.AreEqual(0, constructionsCardsRepository.Count);
            
            pointsService.Dispose();
            constructionsService.Dispose();
        }


        [Test, Order(TestCore.ModelOrder)]
        public void IsOffsetRight()
        {
            {
                var fieldPosition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 1, 1);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new FieldPosition(0, 0), fieldPosition.GetFieldPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new FieldPosition(1, 0), fieldPosition.GetFieldPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new FieldPosition(2, 0), fieldPosition.GetFieldPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 2, 2);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new FieldPosition(0, 0), fieldPosition.GetFieldPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new FieldPosition(1, 0), fieldPosition.GetFieldPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new FieldPosition(2, 0), fieldPosition.GetFieldPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 3, 3);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new FieldPosition(-1, -1), fieldPosition.GetFieldPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new FieldPosition(0, -1), fieldPosition.GetFieldPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new FieldPosition(1, -1), fieldPosition.GetFieldPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldService(1, new IntPoint(100, 100));
                var size = new IntRect(0, 0, 4, 4);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new FieldPosition(-1, -1), fieldPosition.GetFieldPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0.1f), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new FieldPosition(0, -1), fieldPosition.GetFieldPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0.1f), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new FieldPosition(1, -1), fieldPosition.GetFieldPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0.1f), size));
            }
        }

        [Test, Order(TestCore.ModelSecondaryOrder)]
        public void ShrinkIsCorrect()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Repository<Construction>();

            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, fieldService,controls);
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var scheme = new ConstructionScheme(ghostHalfShrinkDistance: 1, ghostShrinkDistance: 4, view: "model");

            var construction = new Construction(scheme, new FieldPosition(0, 0), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostService, constructionsService, gameAssets, controls);

            var viewModel = view.Container.FindView<ConstructionModelView>();

            Assert.AreEqual(1, viewModel.Shrink.Value);

            ghostService.Show(new ConstructionCard(scheme));

            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 1));

            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 2));

            Assert.AreEqual(0.5f, viewModel.Shrink.Value);

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 4));

            Assert.AreEqual(1f, viewModel.Shrink.Value);

            buildingMode.SetTargetPosition(new GameVector3(0, 0, 0));
            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            ghostService.Hide();

            Assert.AreEqual(1f, viewModel.Shrink.Value);


            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
            constructionsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionPlacedInRightPosition()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Repository<Construction>();

            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var ghostService = new GhostService();
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostService, constructionsService, gameAssets, controls);

            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), view.Position.Value);

            viewCollection.Dispose();
            constructionsService.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionSpawnsVisual()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Repository<Construction>();

            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var ghostService = new GhostService();
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostService, constructionsService, gameAssets, controls);

            Assert.IsNotNull(view.Container.FindView<IConstructionModelView>());

            viewCollection.Dispose();
            constructionsService.Dispose();
            controls.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void DropAnimations()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Repository<Construction>();

            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var ghostService = new GhostService();
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostService, constructionsService, gameAssets, controls);

            var modelView = view.Container.FindView<IConstructionModelView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            viewCollection.Dispose();
            constructionsService.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ExplodeAnimations()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Repository<Construction>();

            var fieldService = new FieldService(1, new IntPoint(11, 11));
            var ghostService = new GhostService();
            var constructionsService = new ConstructionsService(constructionsRepository, fieldService);
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");

            var construction = new Construction(scheme, new FieldPosition(1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostService, constructionsService, gameAssets, controls);

            var modelView = view.Container.FindView<IConstructionModelView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            Assert.IsFalse(view.IsDisposed);

            constructionsRepository.Remove(construction);

            Assert.AreEqual("Explode", animator.Animations[2]);
            Assert.AreEqual("Idle", animator.Animations[3]);

            Assert.IsTrue(view.IsDisposed);
            viewCollection.Dispose();
            constructionsService.Dispose();
            controls.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

    }
}
