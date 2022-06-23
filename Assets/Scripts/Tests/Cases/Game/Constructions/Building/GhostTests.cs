using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Resources;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Repositories;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Localization;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
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
            var ghostService = new GhostService();
            var movingService = new GhostMovingService(ghostService, field, controlService);
            
            c.MovePointer(new GameVector3(1,0,1));
            ghostService.Show(new ConstructionCard(new ConstructionScheme()));
            
            Assert.AreEqual(new GameVector3(1,0, 1), ghostService.GetGhost().TargetPosition);
            Assert.AreEqual(new CellPosition(1, 1), ghostService.GetGhost().Position);
            
            c.MovePointer(new GameVector3(2,0,1));
            Assert.AreEqual(new GameVector3(2,0, 1), ghostService.GetGhost().TargetPosition);
            Assert.AreEqual(new CellPosition(2, 1), ghostService.GetGhost().Position);
            
            movingService.Dispose();
            controlService.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsClickBuildConstruction()
        {
            var c = new ControlsMock();
            
            var constructionsRepository = new Repository<Construction>();
            var constructionsCardsRepository = new Repository<ConstructionCard>();

            var controlService = new GameControlsService(c);
            var field = new Field(1, new (11,11));
            var constructionsService = new ConstructionsService(constructionsRepository, field);
            var buildingService = new BuildingService(constructionsRepository, constructionsService);
            
            var ghostService = new GhostService();
            var ghostBuildingService = new GhostBuildingService(ghostService, constructionsService, 
                buildingService, controlService);
            
            var card = new ConstructionCard(new ConstructionScheme());
            constructionsCardsRepository.Add(card);
            c.Click();
            
            Assert.AreEqual(0, constructionsRepository.Count);
            
            ghostService.Show(card);
            
            Assert.AreEqual(0, constructionsRepository.Count);
        
            c.Click();
            
            Assert.AreEqual(1, constructionsRepository.Count);
        
            ghostService.Hide();
            controlService.Dispose();
            constructionsService.Dispose();
            ghostBuildingService.Dispose();
        }
        
        [Test, Order(TestCore.PresenterOrder)]
        public void IsClickDisableBuildingMode()
        {
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var field = new Field();

            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            
            var scheme = new ConstructionScheme();

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            new GhostManagerPresenter(view, ghostService);

            Assert.IsFalse(view.Container.Has<GhostView>());
            
            ghostService.Show(new ConstructionCard(scheme));

            Assert.IsTrue(view.Container.Has<GhostView>());

            ghostService.Hide();

            Assert.IsFalse(view.Container.Has<GhostView>());

            viewCollection.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var constructionsRepository = new Repository<Construction>();

            var scheme = new ConstructionScheme();

            var field = new Field(10, new IntPoint(3, 3));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var constructionService = new ConstructionsService(constructionsRepository, field);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostService, field, constructionsRepository);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            ghostService.Show(new ConstructionCard(scheme));

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            ghostService.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
            constructionService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var constructionsRepository = new Repository<Construction>();

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

            var field = new Field(0.5f, new IntPoint(7, 7));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var constructionService = new ConstructionsService(constructionsRepository, field);
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostService, field, constructionsRepository);

            ghostService.Show(new ConstructionCard(scheme));

            var cells = view.CellsContainer.FindViews<CellView>();
            var highlightedCells = cells
                .Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            buildingMode.SetTargetPosition(new CellPosition(1, 0));

            highlightedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            viewCollection.Dispose();
            constructionService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
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

            var field = new Field(0.5f, new IntPoint(7, 7));
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var viewCollection = new ViewsCollection();

            ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, ghostService, field, CreateAssets());

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            buildingMode.SetTargetPosition(new CellPosition(1, 0));

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var constructionsRepository = new Repository<Construction>();

            var scheme = new ConstructionScheme(requirements: new Requirements() { DownEdge = true });

            var card = new ConstructionCard(scheme);

            var field = new Field(1, new IntPoint(5, 5));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var viewCollection = new ViewsCollection();

            ghostService.Show(card);

            var constructionsService = new ConstructionsService(constructionsRepository, field);

            var view = new ConstructionModelView(viewCollection);
            new GhostModelPresenter(view, ghostService, constructionsService);

            // disallowed because DownEdge enabled.
            Assert.IsFalse(constructionsService.CanPlace(card, new CellPosition(0, 0), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Disallowed", ((AnimatorMock)view.BorderAnimator).Animation);

            buildingMode.SetTargetPosition(new CellPosition(0, -2));
            Assert.IsTrue(constructionsService.CanPlace(card, new CellPosition(0, -2), new FieldRotation()));

            Assert.AreEqual("Dragging", ((AnimatorMock)view.Animator).Animation);
            Assert.AreEqual("Idle", ((AnimatorMock)view.BorderAnimator).Animation);

            viewCollection.Dispose();
            constructionsService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrect()
        {
            var constructionsRepository = new Repository<Construction>();

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement, requirements: new Requirements() { DownEdge = true });

            var field = new Field(1, new IntPoint(5, 5));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var constructionService = new ConstructionsService(constructionsRepository, field);

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostService, field, constructionsRepository);

            ghostService.Show(new ConstructionCard(scheme));

            buildingMode.SetTargetPosition(new CellPosition(0, -1));

            var cells = view.CellsContainer.FindViews<CellView>();
            var actual = ToArray(5, cells);
            var expected = new [,]
            {
                { 1,1,0,0,0 },
                { 1,1,0,0,0 },
                { 1,2,3,0,0 },
                { 1,2,3,0,0 },
                { 1,1,0,0,0 }
            };

            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");

            constructionService.Dispose();
            viewCollection.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsIsColoredRightWithGhost()
        {
            var constructionsRepository = new Repository<Construction>();


            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement, requirements: new Requirements() { DownEdge = true });

            var field = new Field(1, new IntPoint(5, 5));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);
            var constructionService = new ConstructionsService(constructionsRepository, field);
            var viewCollection = new ViewsCollection();

            ghostService.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostService, field, constructionsRepository);

            buildingMode.SetTargetPosition(new CellPosition(0, -1));

            var cells = view.CellsContainer.FindViews<CellView>();
            var actual = ToArray(5, cells);
            var expected = new[,]
            {
                { 1,1,0,0,0 },
                { 1,1,0,0,0 },
                { 1,2,3,0,0 },
                { 1,2,3,0,0 },
                { 1,1,0,0,0 }
            };

            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");

            viewCollection.Dispose();
            constructionService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var field = new Field(1, new IntPoint(3, 3));
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);

            var assets = CreateAssets("view");
            var scheme = new ConstructionScheme(view:"view");

            var viewCollection = new ViewsCollection();
            ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            
            Assert.IsFalse(view.Container.Has<ConstructionModelView>());
            
            new GhostPresenter(view, ghostService, field, assets);

            Assert.IsTrue(view.Container.Has<ConstructionModelView>());
            
            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
        }

        [Test]
        public void IsRotationWorking()
        {
            var constructionsRepository = new Repository<Construction>();

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 0 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            var field = new Field(1, new IntPoint(5, 5));
            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService();
            var buildingMode = new GhostMovingService(ghostService, field,controls);

            var constructionService = new ConstructionsService(constructionsRepository, field);
            var viewCollection = new ViewsCollection();

            ghostService.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostService, field, constructionsRepository);

            buildingMode.SetTargetPosition(new CellPosition(0, 0));

            var cells = view.CellsContainer.FindViews<CellView>();
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Right));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Bottom));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            buildingMode.SetRotation(new FieldRotation(FieldRotation.Rotation.Left));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            viewCollection.Dispose();
            constructionService.Dispose();
            buildingMode.Dispose();
            controls.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(5, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
        }
        
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
    }
}
