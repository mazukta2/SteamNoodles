using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Controls;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Tests.Environment.Game;
using Game.Assets.Scripts.Tests.Setups;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using Game.Assets.Scripts.Tests.Views.Ui.Constructions.Hand;
using Game.Tests.Cases;
using Game.Tests.Mocks.Settings.Levels;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class BuildingTests
    {
        [Test]
        public void IsConstructionPlaced()
        {
            var game = new GameConstructor().Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            Assert.IsNull(game.LevelCollection.FindView<ConstructionView>());

            game.Controls.Click();

            Assert.IsNotNull(game.LevelCollection.FindView<ConstructionView>());

            game.Dispose();
        }


        [Test]
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
            var game = new GameConstructor()
                .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.StartingHand.First().Placement = new int[,]
                    {
                        { 1 },
                        { 1 },
                    };
                })
                .Build();
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            var ghost = game.LevelCollection.FindView<GhostView>();

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

            var construction = game.LevelCollection.FindView<ConstructionView>();
            Assert.AreEqual(new GameVector3(-1.5f, 0, -2), construction.Position.Value);

            game.Dispose();
        }

        [Test]
        public void IsConstructionPlacedHaveRightVisual()
        {
            var imagePath = "HandImage";
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>((d) =>
                {
                    d.StartingHand.First().HandImagePath = imagePath;
                })
                .Build();

            Assert.AreEqual(imagePath, game.LevelCollection.FindView<HandConstructionView>().Image.Path);

            game.Dispose();
        }

        [Test]
        public void IsRemovedFromHand()
        {
            var game = new GameConstructor()
                .UpdateDefinition<LevelDefinitionMock>(x => x.ConstructionsReward = new Dictionary<ConstructionDefinition, int>())
                .Build();

            Assert.AreEqual(1, game.LevelCollection.FindViews<HandConstructionView>().Count);

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();

            Assert.AreEqual(0, game.LevelCollection.FindViews<HandConstructionView>().Count);
            game.Dispose();
        }

        [Test]
        public void IsTwoConstructionsPlacing()
        {
            var construction = ConstructionSetups.GetDefault();
            var game = new GameConstructor()
                .AddDefinition("construction", construction)
                .UpdateDefinition<LevelDefinitionMock>(x => x.StartingHand = new List<ConstructionDefinition>() { construction, construction })
                .UpdateDefinition<ConstructionsSettingsDefinition>(c => c.CellSize = 1)
                .Build();

            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.Click();
            game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();
            game.Controls.MovePointer(new GameVector3(-2, 0, 0));
            game.Controls.Click();

            Assert.AreEqual(2, game.LevelCollection.FindViews<ConstructionView>().Count());

            game.Dispose();
        }

        [Test]
        public void IsRotationWorking()
        {
            //var size = 5;
            //var game = new GameConstructor()
            //    .UpdateDefinition<ConstructionsSettingsDefinition>((d) => d.CellSize = 1)
            //    .UpdateDefinition<LevelDefinitionMock>((d) =>
            //    {
            //        d.PlacementField.Size = new IntPoint(size, size);
            //        d.StartingHand.First().Placement = new int[,]
            //        {
            //            { 1, 1 },
            //            { 1, 0 }
            //        };
            //    })
            //    .Build();

            //game.LevelCollection.FindViews<HandConstructionView>().First().Button.Click();

            //var cells = game.LevelCollection.FindViews<CellView>();
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,2,1,1 },
            //    { 1,1,1,1,1 }
            //});
            //game.Keys.TapKey(GameKeys.RotateRight);
            //Assert.AreEqual(new FieldRotation(FieldRotation.Rotation.Right), game.LevelCollection.FindView<GhostView>().Presenter.Rotation);
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,1,2,1 },
            //    { 1,1,1,1,1 }
            //});
            //game.Keys.TapKey(GameKeys.RotateRight);
            //Assert.AreEqual(new FieldRotation(FieldRotation.Rotation.Bottom), game.LevelCollection.FindView<GhostView>().Presenter.Rotation);
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,1,2,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,1,1,1 }
            //});
            //game.Keys.TapKey(GameKeys.RotateLeft);
            //Assert.AreEqual(new FieldRotation(FieldRotation.Rotation.Right), game.LevelCollection.FindView<GhostView>().Presenter.Rotation);
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,1,2,1 },
            //    { 1,1,1,1,1 }
            //});
            //game.Keys.TapKey(GameKeys.RotateLeft);
            //Assert.AreEqual(new FieldRotation(FieldRotation.Rotation.Top), game.LevelCollection.FindView<GhostView>().Presenter.Rotation);
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,2,1,1 },
            //    { 1,1,1,1,1 }
            //});

            //game.Keys.TapKey(GameKeys.RotateLeft);
            //Assert.AreEqual(new FieldRotation(FieldRotation.Rotation.Left), game.LevelCollection.FindView<GhostView>().Presenter.Rotation);
            //CheckPosition(new int[5, 5]
            //{
            //    { 1,1,1,1,1 },
            //    { 1,1,1,1,1 },
            //    { 1,1,2,1,1 },
            //    { 1,1,2,2,1 },
            //    { 1,1,1,1,1 }
            //});

            //game.Dispose();

            //void CheckPosition(int[,] expected)
            //{
            //    var actual = ToArray(size, cells);
            //    Assert.AreEqual(expected, actual, $"Wrong position : \r\n {PrintPosition(actual)}");
            //}
        }

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
