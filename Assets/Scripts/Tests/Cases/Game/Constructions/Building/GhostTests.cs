using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.DataObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Repositories.Aggregations.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Resources;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class GhostTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsGhostMoving()
        {
            var field = new Field(1, new (11,11));
            var c = new ControlsMock();
            var controlService = new GameControlsService(c);
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field);
            // var movingService = new GhostMovingControlsService(ghost, new SingletonDatabase<Field>(), controlService);
            
            c.MovePointer(new GameVector3(1,0,1));
            // ghostService.Show(new ConstructionCard(new ConstructionScheme()));
            
            Assert.AreEqual(new GameVector3(1,0, 1), ghost.Get().TargetPosition);
            Assert.AreEqual(new FieldPosition(field,1, 1), ghost.Get().Position);
            
            c.MovePointer(new GameVector3(2,0,1));
            Assert.AreEqual(new GameVector3(2,0, 1), ghost.Get().TargetPosition);
            Assert.AreEqual(new FieldPosition(field,2, 1), ghost.Get().Position);
            
            // movingService.Dispose();
            controlService.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsBuildingWorks()
        {
            var constructionsRepository = new Database<Construction>();
            var constructionsCardsRepository = new Database<ConstructionCard>();

            var pointsService = new BuildingPointsService(0, 0, new GameTime(), 2, 2);
            var handService = new HandService(constructionsCardsRepository);
            var field = new Field(1, new IntPoint(11, 11));
            var removeFromHandService = new RemoveCardOnBuildingService(constructionsRepository, handService);

            var card = new ConstructionCard();
            var ghost = new ConstructionGhost(card, field);
            var ghostPlacing = new ConstructionGhostPlacing(ghost, constructionsRepository, field);

            var scheme = new ConstructionScheme();
            constructionsCardsRepository.Add(card);

            Assert.AreEqual(0, constructionsRepository.Count);

            ghost.SetPosition(new FieldPosition(field, 1, 1), GameVector3.Zero);
            ghostPlacing.Build();

            Assert.AreEqual(1, constructionsRepository.Count);
            var construction = constructionsRepository.Get().First();
            Assert.AreEqual(new FieldPosition(field, 1, 1), construction.Position);
            Assert.AreEqual(scheme, construction.Scheme);

            Assert.AreEqual(0, constructionsCardsRepository.Count);
            
            pointsService.Dispose();
            removeFromHandService.Dispose();
        }
        
        [Test, Order(TestCore.ModelOrder)]
        public void IsClickBuildConstruction()
        {
            var c = new ControlsMock();
            
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(1, new (11,11)));
            var constructionsCardsRepository = new Database<ConstructionCard>();
            var ghost = new SingletonDatabase<ConstructionGhost>();

            var ghostCollection = new GhostPresentationRepository();

            var controlService = new GameControlsService(c);
            // var buildingService = new BuildingService(constructionsRepository);
            //
            // var ghostService = new GhostService(ghost, field.Get());
            // var cells = new BuildingAggregatorService(field, ghost, constructionsRepository);
            // var ghostMock = new GhostData();
            
            // var ghostBuildingService = new GhostBuildingService(ghost, ghostCollection, controlService);
            
            var card = new ConstructionCard(new ConstructionScheme());
            constructionsCardsRepository.Add(card);
            c.Click();
            
            Assert.AreEqual(0, constructionsRepository.Count);
            
            // ghostService.Show(card);
            
            Assert.AreEqual(0, constructionsRepository.Count);
        
            c.Click();
            
            Assert.AreEqual(1, constructionsRepository.Count);
        
            controlService.Dispose();
            // ghostBuildingService.Dispose();
            // cells.Dispose();
        }
        
        [Test, Order(TestCore.ModelOrder)]
        public void GhostFillingPoints()
        {
            var constructions = new Database<Construction>();
            var field = new Field();

            // var pointsService = new BuildingAggregatorService(field, ghost, constructions);
            var c = new ConstructionGhost(new ConstructionCard(new ConstructionScheme(points: new BuildingPoints(2))),
                     field);
            var ghost = new ConstructionGhostPlacing(c, constructions, field);

            // ghost.Add();
            
            Assert.AreEqual(2, ghost.GetPoints().AsInt());

            // var g = ghost.Get();
            // g.SetPosition(new FieldPosition(field.Get(), 100, 100), GameVector3.One);
            
            Assert.AreEqual(0, ghost.GetPoints().AsInt());
            
            // pointsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var field = new Field();

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field);
            // var buildingMode = new GhostMovingControlsService(ghost, new SingletonDatabase<Field>(),controls);
            
            var scheme = new ConstructionScheme();

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            // new GhostManagerPresenter(view, new DataProvider<GhostData>());

            Assert.IsFalse(view.Container.Has<GhostView>());
            
            // ghostService.Show(new ConstructionCard(scheme));

            Assert.IsTrue(view.Container.Has<GhostView>());

            // ghostService.Hide();

            Assert.IsFalse(view.Container.Has<GhostView>());

            viewCollection.Dispose();
            // buildingMode.Dispose();
            controls.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(10, new IntPoint(3, 3)));
            var ghost = new SingletonDatabase<ConstructionGhost>();

            var scheme = new ConstructionScheme();

            var controls = new GameControlsService(new ControlsMock());
            // var ghostService = new GhostService(ghost, field.Get());
            // var moving = new GhostMovingControlsService(ghost, field,controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
            //     constructionsRepository);
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            // new PlacementFieldPresenter(view, new DataProvider<GhostData>(), new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            // ghostService.Show(new ConstructionCard(scheme));

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            // ghostService.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
            // moving.Dispose();
            controls.Dispose();
            // fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(0.5f, new IntPoint(7, 7)));

            var placement = new ContructionPlacement(new[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });

            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement,
                LocalizationTag.None,
                new BuildingPoints(0),
                new AdjacencyBonuses());

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field.Get());
            // var movingService = new GhostMovingControlsService(ghost, field,controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
            //     constructionsRepository);
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            // new PlacementFieldPresenter(view, new DataProvider<GhostData>(), new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());
            //
            // ghostService.Show(new ConstructionCard(scheme));

            var cells = view.CellsContainer.FindViews<CellView>();
            var highlightedCells = cells
                .Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            // movingService.SetTargetPosition(new FieldPosition(field.Get(), 1, 0));

            highlightedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            viewCollection.Dispose();
            // movingService.Dispose();
            controls.Dispose();
            // fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(0.5f, new IntPoint(7, 7)));
            
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var placement = new ContructionPlacement(new[,] {
                    { 0, 0, 0 },
                    { 0, 1, 0 },
                    { 0, 1, 0 },
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field.Get());
            // var moving = new GhostMovingControlsService(ghost, field,controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
            //     constructionsRepository);
            
            var viewCollection = new ViewsCollection();

            // ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            // new GhostPresenter(view, new DataProvider<GhostData>(), new DataCollectionProvider<ConstructionPresenterData>(), CreateAssets());

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            // moving.SetTargetPosition(new FieldPosition(field.Get(), 1, 0));

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
            // moving.Dispose();
            // fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void GhostChangesGlowingEffectWhenBlocked()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(1, new IntPoint(5, 5)));
            var ghost = new SingletonDatabase<ConstructionGhost>();

            var scheme = new ConstructionScheme(view:"view", requirements: new Requirements() { DownEdge = true });

            var card = new ConstructionCard(scheme);

            var controls = new GameControlsService(new ControlsMock());
            // var ghostService = new GhostService(ghost, field.Get());
            // var moving = new GhostMovingControlsService(ghost, field,controls);
            // var cells = new BuildingAggregatorService(field, ghost, constructionsRepository);
            // var canBuildService = new BuildingAggregatorService(field, ghost, constructionsRepository);
            // var ghostData = new DataProvider<GhostData>();
            
            var assets = CreateAssets("view");
            var viewCollection = new ViewsCollection();

            // ghostService.Show(card);

            var view = new GhostView(viewCollection);
            // new GhostPresenter(view, ghostData, new DataCollectionProvider<ConstructionPresenterData>(), assets);

            var modelView = view.Container.FindView<ConstructionModelView>();

            // disallowed because DownEdge enabled.
            // Assert.IsFalse(ghostData.Get().CanBuild);

            Assert.AreEqual("Dragging", (modelView.Animator).GetCurrentAnimation());
            Assert.AreEqual("Disallowed", (modelView.BorderAnimator).GetCurrentAnimation());

            // moving.SetTargetPosition(new FieldPosition(field.Get(), 0, -2));
            // Assert.IsTrue(ghostData.Get().CanBuild);

            Assert.AreEqual("Dragging", (modelView.Animator).GetCurrentAnimation());
            Assert.AreEqual("Idle", (modelView.BorderAnimator).GetCurrentAnimation());

            viewCollection.Dispose();
            // moving.Dispose();
            controls.Dispose();
            // cells.Dispose();
            // canBuildService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrectWithDownEdgeRequirement()
        {
            var constructionsRepository = new Database<Construction>();
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var field = new SingletonDatabase<Field>(new Field(1, new IntPoint(5, 5)));

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement, requirements: new Requirements() { DownEdge = true });

            var controls = new GameControlsService(new ControlsMock());
            // var ghostService = new GhostService(ghost, field.Get());
            // var buildingMode = new GhostMovingControlsService(ghost, field,controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
                // constructionsRepository);

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            // new PlacementFieldPresenter(view, new DataProvider<GhostData>(), new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());

            // ghostService.Show(new ConstructionCard(scheme));

            // buildingMode.SetTargetPosition(new FieldPosition(field.Get(), 0, -1));

            var cells = view.CellsContainer.FindViews<CellView>();
            var actual = ToArray(5, cells);
            var expected = new [,]
            {
                { 1,1,4,4,4 },
                { 1,1,4,4,4 },
                { 1,2,3,4,4 },
                { 1,2,3,4,4 },
                { 1,1,4,4,4 }
            };

            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");

            viewCollection.Dispose();
            // buildingMode.Dispose();
            controls.Dispose();
            // fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrectWithOtherConstructions()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(1, new IntPoint(5, 5)));

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field.Get(), 0, 0), FieldRotation.Default));

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field.Get());
            // var moving = new GhostMovingControlsService(ghost, field, controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
                // constructionsRepository);
            
            var viewCollection = new ViewsCollection();

            var view = new PlacementFieldView(viewCollection);
            // new PlacementFieldPresenter(view, new DataProvider<GhostData>(), new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());

            {
                
                var cells = view.CellsContainer.FindViews<CellView>();
                var actual = ToArray(5, cells);
                var expected = new[,]
                {
                    { 0,0,0,0,0 },
                    { 0,0,0,0,0 },
                    { 0,0,4,4,0 },
                    { 0,0,4,4,0 },
                    { 0,0,0,0,0 }
                };

                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
            

            // ghostService.Show(new ConstructionCard(scheme));

            // moving.SetTargetPosition(new FieldPosition(field.Get(), 0, -1));

            {
                var cells = view.CellsContainer.FindViews<CellView>();
                var actual = ToArray(5, cells);
                var expected = new[,]
                {
                    { 1,1,1,1,1 },
                    { 1,1,1,1,1 },
                    { 1,2,3,4,1 },
                    { 1,2,3,4,1 },
                    { 1,1,1,1,1 }
                };

                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }

            // fieldCellsService.Dispose();
            viewCollection.Dispose();
            // moving.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var constructionsRepository = new Database<Construction>();
            
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var field = new Field(1, new IntPoint(3, 3));
            var ghost = new SingletonDatabase<ConstructionGhost>();
            // var ghostService = new GhostService(ghost, field);
            // var buildingMode = new GhostMovingControlsService(ghost, new SingletonDatabase<Field>(),controls);

            var assets = CreateAssets("view");
            var scheme = new ConstructionScheme(view:"view");

            var viewCollection = new ViewsCollection();
            // ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            
            Assert.IsFalse(view.Container.Has<ConstructionModelView>());
            
            // new GhostPresenter(view, new DataProvider<GhostData>(), new DataCollectionProvider<ConstructionPresenterData>(), assets);

            Assert.IsTrue(view.Container.Has<ConstructionModelView>());
            
            viewCollection.Dispose();
            controls.Dispose();
            // buildingMode.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void RotationWorking()
        {
            var constructionsRepository = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(1, new IntPoint(5, 5)));
            var ghost = new SingletonDatabase<ConstructionGhost>();

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 0 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            var controls = new GameControlsService(new ControlsMock());
            // var ghostService = new GhostService(ghost, field.Get());
            // var rotatingService = new GhostRotatingControlsService(ghost, controls);
            // var fieldCellsService = new BuildingAggregatorService(field, ghost,
                // constructionsRepository);

            var viewCollection = new ViewsCollection();

            // ghostService.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            // new PlacementFieldPresenter(view, new DataProvider<GhostData>(), new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());

            var cells = view.CellsContainer.FindViews<CellView>();
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            // rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Right));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });

            // rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Bottom));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            // rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Left));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            viewCollection.Dispose();
            controls.Dispose();
            // rotatingService.Dispose();
            // fieldCellsService.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(5, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
        }
        
        [Test, Order(TestCore.PresenterOrder)]
        public void ShrinkIsCorrect()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            
            // var field = new Field(1, new IntPoint(11, 11));
            // var buildingMode = new GhostMovingService(ghost, new SingletonRepository<Field>(),controls);
            
            var scheme = new ConstructionScheme(ghostHalfShrinkDistance: 1, ghostShrinkDistance: 4, view: "model");
            var construction = new DataProvider<ConstructionPresenterData>(new ConstructionPresenterData(scheme));

            var ghost = new GhostPresentation();
            // var ghostCollection = new GhostRepository(new SingletonDatabase<ConstructionGhost>());
            var ghostPresentationCollection = new GhostPresentationRepository();
            
            var viewCollection = new ViewsCollection();
            
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, construction, ghostPresentationCollection, gameAssets, controls);

            var viewModel = view.Container.FindView<ConstructionModelView>();

            Assert.AreEqual(1, viewModel.Shrink.Value);

            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            // ghostPresentationCollection.Show();
            
            // buildingMode.SetTargetPosition(new GameVector3(0, 0, 1));

            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            // buildingMode.SetTargetPosition(new GameVector3(0, 0, 2));

            Assert.AreEqual(0.5f, viewModel.Shrink.Value);

            // buildingMode.SetTargetPosition(new GameVector3(0, 0, 4));

            Assert.AreEqual(1f, viewModel.Shrink.Value);

            // buildingMode.SetTargetPosition(new GameVector3(0, 0, 0));
            Assert.AreEqual(0.2f, viewModel.Shrink.Value);

            // ghost.Remove();

            Assert.AreEqual(1f, viewModel.Shrink.Value);


            viewCollection.Dispose();
            controls.Dispose();
        }
        
        #region  helpers

        int[,] ToArray(int size, IReadOnlyCollection<CellView> cells)
        {
            var actual = new int[size, size];
            var check = 0;
            for (int x = -size / 2; x <= size / 2; x++)
                for (int y = -size / 2; y <= size / 2; y++)
                {
                    actual[x + size / 2, y + size / 2] = (int)cells.First(c => c.LocalPosition.Value == new GameVector3(x, 0, y)).State.Value;
                    check++;
                }

            Assert.AreEqual(check, cells.Count());
            return actual;
        }

        string PrintPosition(int[,] matrix)
        {
            var result = "";
            for (int x = 0; x < matrix.GetLength(0); x++)
            {
                for (int y = 0; y < matrix.GetLength(1); y++)
                {
                    result += matrix[x, y].ToString();
                }
                result += "\r\n";
            }
            return result;
        }

        private GameAssetsService CreateAssets(string name = "")
        {
            var assets = new AssetsMock();
            assets.AddPrefab(name, new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            return gameAssets;
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
        

        #endregion
    }
}
