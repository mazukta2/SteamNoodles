using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Tests.Views.Ui.Screens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Tests.Definitions;

namespace Game.Assets.Scripts.Tests.Cases.Game.Building
{
    public class BuildingTests
    {
        #region Placement
        [Test]
        public void IsCellCalculationIsCorrect()
        {
            var calculator = new FieldPositionsCalculator(10);
            var size = new IntRect(0, 0, 1, 1);
            Assert.AreEqual(new GameVector3(0, 0, 0), calculator.GetMapPositionByGridPosition(new IntPoint(0, 0), size));
            Assert.AreEqual(new GameVector3(10, 0, -10), calculator.GetMapPositionByGridPosition(new IntPoint(1, -1), size));
        }

        [Test]
        public void IsPlacementCreateCellsUnevenSize()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.MainLevelVariation.PlacementField.Size = new IntPoint(3, 3))
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 10)
                .Build();

            Assert.IsNotNull(game.Views.FindView<PlacementFieldView>());

            var cells = game.Views.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));

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
                Assert.AreEqual(expected, constrcution.GetRect(Scripts.Game.Logic.Models.Constructions.FieldRotation.Top));
            }
        }

        #endregion

        #region BuildingScreen

        [Test]
        public void IsClickingActivatingBuildingScreen()
        {
            var game = new BuildConstructor().Build();

            Assert.IsNotNull(game.Views.FindView<MainScreenView>());
            Assert.IsNotNull(game.Views.FindView<HandView>());
            Assert.IsNull(game.Views.FindView<BuildScreenView>());

            var hand = game.Views.FindView<HandView>();
            var view = game.Views.FindViews<HandConstructionView>().First();
            view.Button.Click();

            Assert.IsNull(game.Views.FindView<MainScreenView>());
            Assert.AreEqual("Build", game.Views.FindView<HandView>().Animator.Animation);
            Assert.IsNotNull(game.Views.FindView<BuildScreenView>());

            hand.CancelButton.Click();

            Assert.IsNotNull(game.Views.FindView<MainScreenView>());
            Assert.AreEqual("Choose", game.Views.FindView<HandView>().Animator.Animation);
            Assert.IsNull(game.Views.FindView<BuildScreenView>());

            game.Dispose();
        }

        [Test]
        public void IsCreatingAGhost()
        {
            var game = new BuildConstructor().Build();

            var manager = game.Views.FindView<GhostManagerView>();
            Assert.IsNotNull(manager);
            Assert.AreEqual(0, game.Views.FindViews<GhostView>().Count);

            var hand = game.Views.FindView<HandView>();
            var view = game.Views.FindViews<HandConstructionView>().First();
            view.Button.Click();

            Assert.AreEqual(1, game.Views.FindViews<GhostView>().Count);

            hand.CancelButton.Click();

            Assert.AreEqual(0, game.Views.FindViews<GhostView>().Count);

            game.Dispose();
        }

        [Test]
        public void IsAvailableCellsIsHighlightedInGhostMode()
        {
            var game = new BuildConstructor()
                .Build();

            var cells = game.Views.FindViews<CellView>();
            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.Normal));

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            Assert.AreEqual(1, game.Views.FindViews<GhostView>().Count);

            Assert.IsTrue(cells.All(x => x.State.Value == CellPlacementStatus.IsReadyToPlace || x.State.Value == CellPlacementStatus.IsAvailableGhostPlace));

            game.Dispose();
        }

        [Test]
        public void IsBuildingPlacingIsExitGhostMode()
        {
            var game = new BuildConstructor().Build();
            
            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            Assert.IsNotNull(game.Views.FindView<GhostView>());

            game.Controls.Click();

            Assert.IsNull(game.Views.FindView<GhostView>());

            game.Dispose();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlighted()
        {
            var game = new BuildConstructor().Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            var cells = game.Views.FindViews<CellView>();
            var highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            game.Controls.MovePointer(new GameVector3(0.75f, 0, 0)); // move left

            highlighedCells = cells.Where(x => x.State.Value == CellPlacementStatus.IsAvailableGhostPlace).OrderBy(x => x.LocalPosition.Value.X);
            Assert.AreEqual(2, highlighedCells.Count());

            Assert.AreEqual(new GameVector3(0.5f, 0, 0), highlighedCells.First().LocalPosition.Value);
            Assert.AreEqual(new GameVector3(1f, 0, 0), highlighedCells.Last().LocalPosition.Value);

            game.Dispose();
        }

        [Test]
        public void IsGhostViewChangeVisualIfItAvailable()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) => d.MainLevelVariation.StartingHand.First().Requirements = new Requirements() {  DownEdge = true })
                .Build();
            var cellSize = 0.5f;

            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            var ghost = game.Views.FindView<GhostView>();
            var model = game.Views.FindView<ConstructionModelView>();
            Assert.AreEqual("Dragging", ((AnimatorMock)model.Animator).Animation);
            Assert.AreEqual("Disallowed", ((AnimatorMock)model.BorderAnimator).Animation);

            var newPos = new GameVector3(0.25f, 0, -4f * cellSize);
            game.Controls.MovePointer(newPos); // move down

            Assert.AreEqual(newPos, ghost.LocalPosition.Value);

            Assert.AreEqual("Dragging", ((AnimatorMock)model.Animator).Animation);
            Assert.AreEqual("Idle", ((AnimatorMock)model.BorderAnimator).Animation);

            game.Dispose();
        }

        [Test]
        public void IsCellsBeneathGhostIsHighlightedRed()
        {
            var size = 5;
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1 )
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.MainLevelVariation.PlacementField.Size = new IntPoint(size, size);
                    d.MainLevelVariation.StartingHand.First().Placement = new int[,]
                    {
                        { 1, 1 },
                        { 1, 1 }
                    };
                    d.MainLevelVariation.StartingHand.First().Requirements = new Requirements() { DownEdge = true };
                })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            var worldPos = new GameVector3(0f, 0, -0.75f);
            game.Controls.MovePointer(worldPos);

            var cells = game.Views.FindViews<CellView>();
            var actual = ToArray(size, cells);
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
        }
        #endregion

        #region Build
        [Test]
        public void IsConstructionPlaced()
        {
            var game = new BuildConstructor().Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            Assert.IsNull(game.Views.FindView<ConstructionView>());

            game.Controls.Click();

            Assert.IsNotNull(game.Views.FindView<ConstructionView>());

            game.Dispose();
        }


        [Test]
        public void IsOffsetRight()
        {
            {
                var fieldPoisition = new FieldPositionsCalculator(1);
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
                var fieldPoisition = new FieldPositionsCalculator(1);
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
                var fieldPoisition = new FieldPositionsCalculator(1);
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
                var fieldPoisition = new FieldPositionsCalculator(1);
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
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.MainLevelVariation.StartingHand.First().Placement = new int[,]
                    {
                        { 1 },
                        { 1 },
                    };
                })
                .Build();
            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            var ghost = game.Views.FindView<GhostView>();

            // -0.5 to 0.5
            game.Controls.MovePointer(new GameVector3(0, 0, 0f));
            Assert.AreEqual(new GameVector3(0.5f, 0, 0), ghost.LocalPosition.Value);

            // -0.5 to -1.5
            game.Controls.MovePointer(new GameVector3(-0.9f, 0, -1f));
            Assert.AreEqual(new GameVector3(-0.5f, 0, -1), ghost.LocalPosition.Value);

            // -1.5 to -2.5
            game.Controls.MovePointer(new GameVector3(-1.9f, 0, -2f));
            Assert.AreEqual(new GameVector3(-1.5f, 0, -2), ghost.LocalPosition.Value);

            game.Controls.Click();

            var construction = game.Views.FindView<ConstructionView>();
            Assert.AreEqual(new GameVector3(-1.5f, 0, -2), construction.Position.Value);

            game.Dispose();
        }

        [Test]
        public void IsConstructionPlacedHaveRightVisual()
        {
            var imagePath = "HandImage";
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.MainLevelVariation.StartingHand.First().HandImagePath = imagePath;
                })
                .Build();

            Assert.AreEqual(imagePath, game.Views.FindView<HandConstructionView>().Image.Path);

            game.Dispose();
        }

        [Test]
        public void IsRemovedFromHand()
        {
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.ConstructionsReward = new Dictionary<ConstructionDefinition,int>())
                .Build();

            Assert.AreEqual(1, game.Views.FindViews<HandConstructionView>().Count);

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0, game.Views.FindViews<HandConstructionView>().Count);
            game.Dispose();
        }

        [Test]
        public void IsTwoConstructionsPlacing()
        {
            var construction = ConstructionSetups.GetDefault();
            var game = new BuildConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.MainLevelVariation.StartingHand = new List<ConstructionDefinition>() { construction, construction })
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            game.Views.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(2, game.Views.FindViews<ConstructionView>().Count());

            game.Dispose();
        }
        #endregion

        #region Rotation
        [Test]
        public void IsRotationWorking()
        {
            var size = 5;
            var game = new BuildConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.MainLevelVariation.PlacementField.Size = new IntPoint(size, size);
                    d.MainLevelVariation.StartingHand.First().Placement = new int[,]
                    {
                        { 1, 1 },
                        { 1, 0 }
                    };
                })
                .Build();

            game.Views.FindViews<HandConstructionView>().First().Button.Click();

            var cells = game.Views.FindViews<CellView>();
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });
            game.Keys.TapKey(GameKeys.RotateRight);
            Assert.AreEqual(FieldRotation.Right, game.Views.FindView<GhostView>().Presenter.Rotation);
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });
            game.Keys.TapKey(GameKeys.RotateRight);
            Assert.AreEqual(FieldRotation.Bottom, game.Views.FindView<GhostView>().Presenter.Rotation);
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,1,2,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });
            game.Keys.TapKey(GameKeys.RotateLeft);
            Assert.AreEqual(FieldRotation.Right, game.Views.FindView<GhostView>().Presenter.Rotation);
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,2,1 },
                { 1,1,1,1,1 }
            });
            game.Keys.TapKey(GameKeys.RotateLeft);
            Assert.AreEqual(FieldRotation.Top, game.Views.FindView<GhostView>().Presenter.Rotation);
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,2,1 },
                { 1,1,2,1,1 },
                { 1,1,1,1,1 }
            });

            game.Keys.TapKey(GameKeys.RotateLeft);
            Assert.AreEqual(FieldRotation.Left, game.Views.FindView<GhostView>().Presenter.Rotation);
            CheckPosition(new int[5, 5]
            {
                { 1,1,1,1,1 },
                { 1,1,1,1,1 },
                { 1,1,2,1,1 },
                { 1,1,2,2,1 },
                { 1,1,1,1,1 }
            });

            game.Dispose();

            void CheckPosition(int[,] expected)
            {
                var actual = ToArray(size, cells);
                Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            }
        }

        #endregion

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

        int[,] ToArray(int size, IReadOnlyCollection<CellView> cells)
        {
            var actual = new int[size, size];
            var chkd = 0;
            for (int x = -size / 2; x <= size / 2; x++)
                for (int y = -size / 2; y <= size / 2; y++)
                {
                    actual[x + size / 2, y + size / 2] = (int)cells.First(c => c.LocalPosition.Value == new GameVector3(x, 0, y)).State.Value;
                    chkd++;
                }

            Assert.AreEqual(chkd, cells.Count());
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
    }
}
