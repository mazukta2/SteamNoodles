using Assets.Scripts.Logic.Prototypes.Levels;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using Game.Assets.Scripts.Tests.Managers.Game;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Building
{
    public class BuildingTests
    {
        #region Placement
        [Test]
        public void IsPlacementCreateCells()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.PlacementFields.First().Size = new IntPoint(2, 2))
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 10)
                .Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<PlacementManagerView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<PlacementFieldView>());

            var cells = game.CurrentLevel.FindViews<CellView>();
            Assert.AreEqual(4, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(5, 5)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(-5, 5)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(5, -5)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(-5, -5)));

            game.Dispose();
        }

        [Test]
        public void IsPlacementCreateCellsUnevenSize()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.PlacementFields.First().Size = new IntPoint(3, 3))
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 10)
                .Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<PlacementManagerView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<PlacementFieldView>());

            var cells = game.CurrentLevel.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(10, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(-10, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(10, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new FloatPoint(-10, -10)));

            game.Dispose();
        }

        [Test]
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


            TestRect(new IntRect(0,0, 3, 3),
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
                var constrcution = new ConstructionDefinition()
                {
                    Placement = rect,
                };
                Assert.AreEqual(expected, constrcution.GetRect());
            }
        }

        #endregion

        #region BuildingScreen

        [Test]
        public void IsClickingActivatingBuildingScreen()
        {
            var game = new GameTestConstructor().Build();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<HandView>());
            Assert.IsNull(game.CurrentLevel.FindView<BuildScreenView>());

            var hand = game.CurrentLevel.FindView<HandView>();
            var view = hand.Cards.Get<HandConstructionView>().First();
            view.Button.Click();

            Assert.IsNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNull(game.CurrentLevel.FindView<HandView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<BuildScreenView>());

            var buildScreen = game.CurrentLevel.FindView<BuildScreenView>();
            buildScreen.CancelButton.Click();

            Assert.IsNotNull(game.CurrentLevel.FindView<MainScreenView>());
            Assert.IsNotNull(game.CurrentLevel.FindView<HandView>());
            Assert.IsNull(game.CurrentLevel.FindView<BuildScreenView>());

            game.Dispose();
        }

        [Test]
        public void IsCreatingAGhost()
        {
            var game = new GameTestConstructor().Build();

            var manager = game.CurrentLevel.FindView<GhostManagerView>();
            Assert.IsNotNull(manager);
            Assert.AreEqual(0, manager.Container.Get<GhostView>().Count);

            var hand = game.CurrentLevel.FindView<HandView>();
            var view = hand.Cards.Get<HandConstructionView>().First();
            view.Button.Click();

            Assert.AreEqual(1, manager.Container.Get<GhostView>().Count);

            var buildScreen = game.CurrentLevel.FindView<BuildScreenView>();
            buildScreen.CancelButton.Click();

            Assert.AreEqual(0, manager.Container.Get<GhostView>().Count);

            game.Dispose();
        }

        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var game = new GameTestConstructor()
                .Build();

            var cells = game.CurrentLevel.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State == CellPlacementStatus.Normal));

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            Assert.IsTrue(cells.All(x => x.State == CellPlacementStatus.IsReadyToPlace || x.State == CellPlacementStatus.IsAvailableGhostPlace));

            game.Dispose();
        }



        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            var game = new GameTestConstructor().Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            Assert.IsNotNull(game.CurrentLevel.FindView<GhostView>());

            game.Engine.Controls.Click();

            Assert.IsNull(game.CurrentLevel.FindView<GhostView>());

            game.Dispose();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var game = new GameTestConstructor().Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            var cells = game.CurrentLevel.FindViews<CellView>();
            var highlighedCells = cells.Where(x => x.State == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new FloatPoint(0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new FloatPoint(0.5f, 0), highlighedCells.Last().LocalPosition.Value);

            game.Engine.Controls.MovePointer(new FloatPoint(0.75f, 0)); // move left

            highlighedCells = cells.Where(x => x.State == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new FloatPoint(0.5f, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new FloatPoint(1f, 0), highlighedCells.Last().LocalPosition.Value);

            game.Dispose();
        }

        [Test]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.StartingHand.First().Requirements = new Requirements() {  DownEdge = true })
                .Build();
            var cellSize = 0.5f;

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            var ghost = game.CurrentLevel.FindView<GhostView>();
            Assert.IsFalse(ghost.CanPlace);

            var newPos = new FloatPoint(0, -4f * cellSize);
            game.Engine.Controls.MovePointer(newPos); // move down

            Assert.AreEqual(newPos, ghost.LocalPosition.Value);

            Assert.IsTrue(ghost.CanPlace);

            game.Dispose();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlightedRed()
        {
            var size = 5;
            var game = new GameTestConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1 )
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.PlacementFields.First().Size = new IntPoint(size, size);
                    d.StartingHand.First().Placement = new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 }
                    };
                    d.StartingHand.First().Requirements = new Requirements() { DownEdge = true };
                })
                .Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            var worldPos = new FloatPoint(0f, -0.75f);
            game.Engine.Controls.MovePointer(worldPos);

            var cells = game.CurrentLevel.FindViews<CellView>();
            var actual = ToArray(cells);
            var expected = new int[5, 5]
            {
                { 1,1,0,0,0 },
                { 1,1,0,0,0 },
                { 1,2,3,0,0 },
                { 1,2,3,0,0 },
                { 1,1,0,0,0 }
            };
            
            Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            game.Dispose();

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

            int[,] ToArray(IReadOnlyCollection<CellView> cells)
            {
                var actual = new int[size, size];
                var chkd = 0;
                for (int x = -size / 2; x <= size / 2; x++)
                    for (int y = -size / 2; y <= size / 2; y++)
                    {
                        actual[x + size / 2, y + size / 2] = (int)cells.First(c => c.LocalPosition.Value == new FloatPoint(x, y)).State;
                        chkd++;
                    }

                Assert.AreEqual(chkd, cells.Count());
                return actual;
            }
        }
        #endregion

        #region Build
        [Test]
        public void IsConstructionPlaced()
        {
            var game = new GameTestConstructor().Build();

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            Assert.IsNull(game.CurrentLevel.FindView<ConstructionView>());

            game.Engine.Controls.Click();

            Assert.IsNotNull(game.CurrentLevel.FindView<ConstructionView>());

            game.Dispose();
        }

        [Test]
        public void IsConstructionPlacedInRightPosition()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
                .Build();
            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();

            var worldPos = new FloatPoint(0, -2);
            game.Engine.Controls.MovePointer(worldPos);

            var ghost = game.CurrentLevel.FindView<GhostView>();
            Assert.AreEqual(worldPos, ghost.LocalPosition.Value);

            game.Engine.Controls.Click();

            var construction = game.CurrentLevel.FindView<ConstructionView>();
            Assert.AreEqual(worldPos, construction.LocalPosition.Value);

            game.Dispose();
        }

        [Test]
        public void IsConstructionPlacedHaveRightVisual()
        {
            var imagePath = "HandImage";
            var game = new GameTestConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.StartingHand.First().HandImagePath = imagePath;
                })
                .Build();

            Assert.AreEqual(imagePath, game.CurrentLevel.FindView<HandConstructionView>().Image.Path);

            game.Dispose();
        }

        [Test]
        public void IsRemovedFromHand()
        {
            var game = new GameTestConstructor()
                .UpdateDefinition<CustomerDefinition>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition,int>())
                .Build();

            Assert.AreEqual(1, game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().Count);
            Assert.AreEqual(1, game.CurrentLevel.FindViews<HandConstructionView>().Count);

            game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().First().Button.Click();
            game.Engine.Controls.Click();

            Assert.AreEqual(0, game.CurrentLevel.FindView<HandView>().Cards.Get<HandConstructionView>().Count);
            Assert.AreEqual(0, game.CurrentLevel.FindViews<HandConstructionView>().Count);
            game.Dispose();
        }
        #endregion

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }
    }
}
