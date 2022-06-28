﻿using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;
using Game.Assets.Scripts.Tests.Setups;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class ConstructionsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsCellCalculationIsCorrect()
        {
            var calculator = new FieldEntity(10, new IntPoint(101, 101));
            var size = new IntRect(0, 0, 1, 1);
            Assert.AreEqual(new GameVector3(0, 0, 0), new FieldPosition(calculator, 0, 0).GetWorldPosition(size));
            Assert.AreEqual(new GameVector3(10, 0, -10), new FieldPosition(calculator, 1, -1).GetWorldPosition(size));
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
        public void PlacementBoundariesRequestRight()
        {
            var setup = new BuildingSetup().Fill(new FieldEntity(10, new IntPoint(3, 3)));
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            
            new PlacementFieldPresenter(view, setup.GhostRepository, setup.FieldRepository.Get(),
                setup.ConstructionsRepository);

            Assert.AreEqual(9, setup.FieldRepository.Get().GetCellsPositions().Cells.Count);

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));
            
            viewCollection.Dispose();
            setup.Dispose();
        }



        [Test, Order(TestCore.ModelOrder)]
        public void IsOffsetRight()
        {
            {
                var fieldPosition = new FieldEntity(1, new IntPoint(101, 101));
                var size = new IntRect(0, 0, 1, 1);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 0, 0), fieldPosition.GetFieldPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 1, 0), fieldPosition.GetFieldPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 2, 0), fieldPosition.GetFieldPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldEntity(1, new IntPoint(101, 101));
                var size = new IntRect(0, 0, 2, 2);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 0, 0), fieldPosition.GetFieldPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 1, 0), fieldPosition.GetFieldPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 2, 0), fieldPosition.GetFieldPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldEntity(1, new IntPoint(101, 101));
                var size = new IntRect(0, 0, 3, 3);

                // from -0.5 to 0.5 its 0, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, -1, -1), fieldPosition.GetFieldPosition(new GameVector3(0, 0, 0), size));
                Assert.AreEqual(new GameVector3(0, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0, 0, 0), size));

                // from 0.5 to 1.5 its 1, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 0, -1), fieldPosition.GetFieldPosition(new GameVector3(0.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(1, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(0.6f, 0, 0), size));

                // from 1.5 to 2.5 its 2, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 1, -1), fieldPosition.GetFieldPosition(new GameVector3(1.6f, 0, 0), size));
                Assert.AreEqual(new GameVector3(2, 0, 0), fieldPosition.GetAlignWithAGrid(new GameVector3(1.6f, 0, 0), size));
            }

            {
                var fieldPosition = new FieldEntity(1, new IntPoint(101, 101));
                var size = new IntRect(0, 0, 4, 4);

                // from 0 to 1 its 0, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, -1, -1), fieldPosition.GetFieldPosition(new GameVector3(0.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(0.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(0.1f, 0, 0.1f), size));

                // from 1 to 2 its 1, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 0, -1), fieldPosition.GetFieldPosition(new GameVector3(1.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(1.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(1.1f, 0, 0.1f), size));

                // from 2 to 3 its 2, 0
                Assert.AreEqual(new FieldPosition(fieldPosition, 1, -1), fieldPosition.GetFieldPosition(new GameVector3(2.1f, 0, 0.1f), size));
                Assert.AreEqual(new GameVector3(2.5f, 0, 0.5f), fieldPosition.GetAlignWithAGrid(new GameVector3(2.1f, 0, 0.1f), size));
            }
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void ConstructionPlacedInRightPosition()
        {
            var setup = new BuildingSetup()
                .Fill(new FieldEntity(1, new IntPoint(11, 11)))
                .FillDefaultModel();
            
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionSchemeEntity(new Uid(), placement: placement, view: "model");
            var construction = new ConstructionEntity(scheme, 
                new FieldPosition(setup.FieldDatabase.Get(), 1, 1), 
                new FieldRotation());
            setup.ConstructionsDatabase.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, setup.GetConstructionViewModel(construction.Id));

            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), view.Position.Value);

            viewCollection.Dispose();
            
            setup.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ConstructionSpawnsVisual()
        {
            var setup = new BuildingSetup()
                .Fill(new FieldEntity(1, new IntPoint(11, 11)))
                .FillDefaultModel();
            
            var placement = new ContructionPlacement(new[,]
            {
                { 1 },
                { 1 },
            });
            var scheme = new ConstructionSchemeEntity(new Uid(), placement: placement, view: "model");
            var construction = new ConstructionEntity(scheme,
                new FieldPosition(setup.FieldDatabase.Get(), 1, 1), 
                new FieldRotation());
            setup.ConstructionsDatabase.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, setup.GetConstructionViewModel(construction.Id));

            Assert.IsNotNull(view.Container.FindView<IConstructionVisualView>());

            viewCollection.Dispose();
            setup.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void DropAnimations()
        {
            var setup = new BuildingSetup()
                .Fill(new FieldEntity(1, new IntPoint(11, 11)))
                .FillDefaultModel();
            
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionSchemeEntity(new Uid(), placement: placement, view: "model");
            var construction = new ConstructionEntity(scheme, new FieldPosition(setup.FieldDatabase.Get(), 1, 1),
                new FieldRotation());
            setup.ConstructionsDatabase.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, setup.GetConstructionViewModel(construction.Id));

            var modelView = view.Container.FindView<IConstructionVisualView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            viewCollection.Dispose();
            setup.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ExplodeAnimations()
        {
            var setup = new BuildingSetup()
                .Fill(new FieldEntity(1, new IntPoint(11, 11)))
                .FillDefaultModel();
            
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionSchemeEntity(new Uid(), placement: placement, view: "model");

            var construction = new ConstructionEntity(scheme, new FieldPosition(setup.FieldDatabase.Get(), 1, 1), new FieldRotation());
            setup.ConstructionsDatabase.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, setup.GetConstructionViewModel(construction.Id));

            var modelView = view.Container.FindView<IConstructionVisualView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            Assert.IsFalse(view.IsDisposed);

            setup.ConstructionsDatabase.Remove(construction.Id);

            Assert.AreEqual("Explode", animator.Animations[2]);
            Assert.AreEqual("Idle", animator.Animations[3]);

            Assert.IsTrue(view.IsDisposed);
            viewCollection.Dispose();
            setup.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void PlacementFieldSpawnsConstructions()
        {
            var setup = new BuildingSetup().FillDefault();
            
            var viewCollection = new ViewsCollection();

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, setup.GhostRepository, 
                setup.FieldRepository.Get(),
                setup.ConstructionsRepository);
            
            Assert.IsFalse(view.ConstrcutionContainer.Has<IConstructionView>());

            setup.ConstructionsDatabase.Add(new ConstructionEntity());
            
            Assert.IsTrue(view.ConstrcutionContainer.Has<IConstructionView>());
            
            viewCollection.Dispose();
            setup.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

    }
}
