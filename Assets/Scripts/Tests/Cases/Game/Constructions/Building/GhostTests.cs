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
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Functions.Constructions;
using Game.Assets.Scripts.Game.Logic.Functions.Repositories;
using Game.Assets.Scripts.Game.Logic.Models.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Models.Services.Constructions.Ghost;
using Game.Assets.Scripts.Game.Logic.Models.Services.Fields;
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
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field);
            var movingService = new GhostMovingService(ghost, field.AsQuery(), controlService);
            
            c.MovePointer(new GameVector3(1,0,1));
            ghostService.Show(new ConstructionCard(new ConstructionScheme()));
            
            Assert.AreEqual(new GameVector3(1,0, 1), ghost.Get().TargetPosition);
            Assert.AreEqual(new FieldPosition(field,1, 1), ghost.Get().Position);
            
            c.MovePointer(new GameVector3(2,0,1));
            Assert.AreEqual(new GameVector3(2,0, 1), ghost.Get().TargetPosition);
            Assert.AreEqual(new FieldPosition(field,2, 1), ghost.Get().Position);
            
            movingService.Dispose();
            controlService.Dispose();
        }

        [Test, Order(TestCore.ModelOrder)]
        public void IsClickBuildConstruction()
        {
            var c = new ControlsMock();
            
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new (11,11)));
            var constructionsCardsRepository = new Repository<ConstructionCard>();

            var controlService = new GameControlsService(c);
            var buildingService = new BuildingService(constructionsRepository);
            
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var cells = new FieldCellsService(field.AsQuery(), ghost.AsQuery(), constructionsRepository.AsQuery());
            var ghostBuildingService = new GhostBuildingService(ghost, buildingService, controlService);
            
            var card = new ConstructionCard(new ConstructionScheme());
            constructionsCardsRepository.Add(card);
            c.Click();
            
            Assert.AreEqual(0, constructionsRepository.Count);
            
            ghostService.Show(card);
            
            Assert.AreEqual(0, constructionsRepository.Count);
        
            c.Click();
            
            Assert.AreEqual(1, constructionsRepository.Count);
        
            controlService.Dispose();
            ghostBuildingService.Dispose();
            cells.Dispose();
        }
        
        [Test, Order(TestCore.ModelOrder)]
        public void GhostFillingPoints()
        {
            var ghost = new SingletonRepository<ConstructionGhost>();
            var constructions = new Repository<Construction>();
            var field = new Field();

            var pointsService = new GhostPointsService(ghost, constructions.AsQuery());

            ghost.Add(new ConstructionGhost(new ConstructionCard(new ConstructionScheme(points: new BuildingPoints(2))),
                field));
            
            Assert.AreEqual(2, ghost.Get().PointChanges.AsInt());

            var g = ghost.Get();
            g.SetPosition(new FieldPosition(field, 100, 100), GameVector3.One);
            
            Assert.AreEqual(0, ghost.Get().PointChanges.AsInt());
            
            pointsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostCreatedInBuildingMode()
        {
            var field = new Field();

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field);
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(),controls);
            
            var scheme = new ConstructionScheme();

            var viewCollection = new ViewsCollection();
            var view = new GhostManagerView(viewCollection);
            new GhostManagerPresenter(view, ghost.AsQuery());

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
            var field = new SingletonRepository<Field>(new Field(10, new IntPoint(3, 3)));

            var scheme = new ConstructionScheme();

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var moving = new GhostMovingService(ghost, field.AsQuery(),controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghost.AsQuery(), field.AsQuery(), constructionsRepository.AsQuery());

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            ghostService.Show(new ConstructionCard(scheme));

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            ghostService.Hide();

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            viewCollection.Dispose();
            moving.Dispose();
            controls.Dispose();
            fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(0.5f, new IntPoint(7, 7)));

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
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var movingService = new GhostMovingService(ghost, field.AsQuery(),controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghost.AsQuery(), field.AsQuery(), constructionsRepository.AsQuery());

            ghostService.Show(new ConstructionCard(scheme));

            var cells = view.CellsContainer.FindViews<CellView>();
            var highlightedCells = cells
                .Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            movingService.SetTargetPosition(new FieldPosition(field.Get(), 1, 0));

            highlightedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace)
                .OrderBy(x => x.LocalPosition.Value.X).ToArray();
            Assert.AreEqual(2, highlightedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlightedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlightedCells.Last().LocalPosition.Value);

            viewCollection.Dispose();
            movingService.Dispose();
            controls.Dispose();
            fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostReactsToChangingPosition()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(0.5f, new IntPoint(7, 7)));
            
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

            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var moving = new GhostMovingService(ghost, field.AsQuery(),controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());
            
            var viewCollection = new ViewsCollection();

            ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), CreateAssets());

            Assert.AreEqual(new GameVector3(0.25f, 0f, 0), view.LocalPosition.Value);

            moving.SetTargetPosition(new FieldPosition(field.Get(), 1, 0));

            Assert.AreEqual(new GameVector3(0.75f, 0f, 0), view.LocalPosition.Value);

            viewCollection.Dispose();
            controls.Dispose();
            moving.Dispose();
            fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void GhostChangesGlowingEffectWhenBlocked()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new IntPoint(5, 5)));
            var ghost = new SingletonRepository<ConstructionGhost>();

            var scheme = new ConstructionScheme(view:"view", requirements: new Requirements() { DownEdge = true });

            var card = new ConstructionCard(scheme);

            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService(ghost, field.Get());
            var moving = new GhostMovingService(ghost, field.AsQuery(),controls);
            var cells = new FieldCellsService(field.AsQuery(), ghost.AsQuery(), constructionsRepository.AsQuery());
            var canBuildService = new GhostCanBuildService(field.AsQuery(), ghost.AsQuery(), constructionsRepository.AsQuery());

            var assets = CreateAssets("view");
            var viewCollection = new ViewsCollection();

            ghostService.Show(card);

            var view = new GhostView(viewCollection);
            new GhostPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), assets);

            var modelView = view.Container.FindView<ConstructionModelView>();

            // disallowed because DownEdge enabled.
            Assert.IsFalse(ghost.Get().CanPlace());

            Assert.AreEqual("Dragging", (modelView.Animator).GetCurrentAnimation());
            Assert.AreEqual("Disallowed", (modelView.BorderAnimator).GetCurrentAnimation());

            moving.SetTargetPosition(new FieldPosition(field.Get(), 0, -2));
            Assert.IsTrue(ghost.Get().CanPlace());

            Assert.AreEqual("Dragging", (modelView.Animator).GetCurrentAnimation());
            Assert.AreEqual("Idle", (modelView.BorderAnimator).GetCurrentAnimation());

            viewCollection.Dispose();
            moving.Dispose();
            controls.Dispose();
            cells.Dispose();
            canBuildService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrectWithDownEdgeRequirement()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new IntPoint(5, 5)));

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement, requirements: new Requirements() { DownEdge = true });

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(),controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());

            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghost.AsQuery(), field.AsQuery(), constructionsRepository.AsQuery());

            ghostService.Show(new ConstructionCard(scheme));

            buildingMode.SetTargetPosition(new FieldPosition(field.Get(), 0, -1));

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
            buildingMode.Dispose();
            controls.Dispose();
            fieldCellsService.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void CellsStatusCorrectWithOtherConstructions()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new IntPoint(5, 5)));

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 1 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            constructionsRepository.Add(new Construction(scheme, new FieldPosition(field.Get(), 0, 0), FieldRotation.Default));

            var controls = new GameControlsService(new ControlsMock());
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field.Get());
            var moving = new GhostMovingService(ghost, field.AsQuery(), controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());
            
            var viewCollection = new ViewsCollection();

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghost.AsQuery(), field.AsQuery(), constructionsRepository.AsQuery());

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
            

            ghostService.Show(new ConstructionCard(scheme));

            moving.SetTargetPosition(new FieldPosition(field.Get(), 0, -1));

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

            fieldCellsService.Dispose();
            viewCollection.Dispose();
            moving.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsGhostSpawnsRightModel()
        {
            var constructionsRepository = new Repository<Construction>();
            
            var mockControls = new ControlsMock();
            var controls = new GameControlsService(mockControls);

            var field = new Field(1, new IntPoint(3, 3));
            var ghost = new SingletonRepository<ConstructionGhost>();
            var ghostService = new GhostService(ghost, field);
            var buildingMode = new GhostMovingService(ghost, field.AsQuery(),controls);

            var assets = CreateAssets("view");
            var scheme = new ConstructionScheme(view:"view");

            var viewCollection = new ViewsCollection();
            ghostService.Show(new ConstructionCard(scheme));

            var view = new GhostView(viewCollection);
            
            Assert.IsFalse(view.Container.Has<ConstructionModelView>());
            
            new GhostPresenter(view, ghost.AsQuery(), constructionsRepository.AsQuery(), assets);

            Assert.IsTrue(view.Container.Has<ConstructionModelView>());
            
            viewCollection.Dispose();
            controls.Dispose();
            buildingMode.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void RotationWorking()
        {
            var constructionsRepository = new Repository<Construction>();
            var field = new SingletonRepository<Field>(new Field(1, new IntPoint(5, 5)));
            var ghost = new SingletonRepository<ConstructionGhost>();

            var placement = new ContructionPlacement(new[,] {
                    { 1, 1 },
                    { 1, 0 }
                });
            var scheme = new ConstructionScheme(new Uid(),
                DefId.None,
                placement);

            var controls = new GameControlsService(new ControlsMock());
            var ghostService = new GhostService(ghost, field.Get());
            var rotatingService = new GhostRotatingService(ghost, controls);
            var fieldCellsService = new FieldCellsService(field.AsQuery(), ghost.AsQuery(),
                constructionsRepository.AsQuery());

            var viewCollection = new ViewsCollection();

            ghostService.Show(new ConstructionCard(scheme));

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghost.AsQuery(), field.AsQuery(), constructionsRepository.AsQuery());

            var cells = view.CellsContainer.FindViews<CellView>();
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Right));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });

            rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Bottom));
            CheckPosition(new [, ]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            rotatingService.SetRotation(new FieldRotation(FieldRotation.Rotation.Left));
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
            rotatingService.Dispose();
            fieldCellsService.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(5, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
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
