using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Common.Time;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Levels.Managing;
using Game.Assets.Scripts.Tests.Environment;
using Game.Assets.Scripts.Tests.Setups.Prefabs.Levels;
using Game.Assets.Scripts.Tests.Views.Common;
using Game.Assets.Scripts.Tests.Views.Level;
using Game.Assets.Scripts.Tests.Views.Level.Building;
using NUnit.Framework;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Aggregations.Constructions.Ghosts;
using Game.Assets.Scripts.Game.Logic.Databases;
using Game.Assets.Scripts.Game.Logic.DataObjects;
using Game.Assets.Scripts.Game.Logic.DataObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.DataObjects.Fields;
using Game.Assets.Scripts.Game.Logic.Entities.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building;
using Game.Assets.Scripts.Game.Logic.Repositories.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Assets;
using Game.Assets.Scripts.Game.Logic.Services.Constructions;
using Game.Assets.Scripts.Game.Logic.Services.Controls;
using Game.Assets.Scripts.Game.Logic.Services.Resources.Points;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Common;
using Game.Assets.Scripts.Game.Logic.ValueObjects.Constructions;
using Game.Assets.Scripts.Game.Logic.Views.Levels;

namespace Game.Assets.Scripts.Tests.Cases.Game.Constructions.Building
{
    public class ConstructionsTests
    {
        [Test, Order(TestCore.ModelOrder)]
        public void IsCellCalculationIsCorrect()
        {
            var calculator = new Field(10, new IntPoint(101, 101));
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
        public void IsPlacementBoundariesRequestRight()
        {
            var constructions = new Database<Construction>();
            var field = new SingletonDatabase<Field>(new Field(10, new IntPoint(3, 3)));
            
            
            var viewCollection = new ViewsCollection();
            var view = new PlacementFieldView(viewCollection);
            var ghost = new GhostPresentation();
            var ghostCollection = new GhostPresentationRepository();
            

            new PlacementFieldPresenter(view, ghostCollection, new DataProvider<FieldData>(), new DataCollectionProvider<ConstructionPresenterData>());

            var cells = view.CellsContainer.FindViews<CellView>();
            Assert.AreEqual(9, cells.Count());

            viewCollection.Dispose();

            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, 10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(10, 0, -10)));
            Assert.AreEqual(1, cells.Count(x => x.LocalPosition.Value == new GameVector3(-10, 0, -10)));
        }



        [Test, Order(TestCore.ModelOrder)]
        public void IsOffsetRight()
        {
            {
                var fieldPosition = new Field(1, new IntPoint(101, 101));
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
                var fieldPosition = new Field(1, new IntPoint(101, 101));
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
                var fieldPosition = new Field(1, new IntPoint(101, 101));
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
                var fieldPosition = new Field(1, new IntPoint(101, 101));
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
        public void IsConstructionPlacedInRightPosition()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Database<Construction>();

            var field = new Field(1, new IntPoint(11, 11));
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");
            var ghostPresentationCollection = new GhostPresentationRepository();

            var construction = new Construction(scheme, new FieldPosition(field, 1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, new DataProvider<ConstructionPresenterData>(),
                ghostPresentationCollection,  gameAssets, controls);

            Assert.AreEqual(new GameVector3(1.5f, 0, 1f), view.Position.Value);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void IsConstructionSpawnsVisual()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Database<Construction>();

            var field = new Field(1, new IntPoint(11, 11));
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");
            var ghostPresentationCollection = new GhostPresentationRepository();

            var construction = new Construction(scheme, new FieldPosition(field, 1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view,  new DataProvider<ConstructionPresenterData>(), 
                ghostPresentationCollection, gameAssets, controls);

            Assert.IsNotNull(view.Container.FindView<IConstructionModelView>());

            viewCollection.Dispose();
            controls.Dispose();
        }


        [Test, Order(TestCore.PresenterOrder)]
        public void DropAnimations()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Database<Construction>();

            var field = new Field(1, new IntPoint(11, 11));
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");
            var ghostPresentationCollection = new GhostPresentationRepository();

            var construction = new Construction(scheme, new FieldPosition(field, 1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, new DataProvider<ConstructionPresenterData>(), 
                ghostPresentationCollection,  gameAssets, controls);

            var modelView = view.Container.FindView<IConstructionModelView>();
            var animator = ((AnimatorMock)modelView.Animator);

            Assert.AreEqual("Drop", animator.Animations[0]);
            Assert.AreEqual("Idle", animator.Animations[1]);

            viewCollection.Dispose();
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void ExplodeAnimations()
        {
            var controls = new GameControlsService(new ControlsMock());
            var assets = new AssetsMock();
            assets.AddPrefab("model", new DefaultViewPrefab(x => new ConstructionModelView(x)));
            var gameAssets = new GameAssetsService(assets);
            var constructionsRepository = new Database<Construction>();

            var field = new Field(1, new IntPoint(11, 11));
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var placement = new ContructionPlacement(new[,]
                    {
                        { 1 },
                        { 1 },
                    });
            var scheme = new ConstructionScheme(new Uid(), placement: placement, view: "model");
            var ghostPresentationCollection = new GhostPresentationRepository();

            var construction = new Construction(scheme, new FieldPosition(field, 1, 1), new FieldRotation());
            constructionsRepository.Add(construction);

            var viewCollection = new ViewsCollection();
            var view = new ConstructionView(viewCollection);
            new ConstructionPresenter(view, new DataProvider<ConstructionPresenterData>(), 
                ghostPresentationCollection, gameAssets, controls);

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
            controls.Dispose();
        }

        [Test, Order(TestCore.PresenterOrder)]
        public void PlacementFieldSpawnsConstructions()
        {
            var constructionsRepository = new Database<Construction>();
            var ghost = new SingletonDatabase<ConstructionGhost>();
            var field = new FieldData();
            
            var viewCollection = new ViewsCollection();
            var ghostPresentationCollection = new GhostPresentationRepository();

            var view = new PlacementFieldView(viewCollection);
            new PlacementFieldPresenter(view, ghostPresentationCollection, 
                new DataProvider<FieldData>(),
                new DataCollectionProvider<ConstructionPresenterData>());
            
            Assert.IsFalse(view.ConstrcutionContainer.Has<IConstructionView>());

            constructionsRepository.Add(new Construction(new Field()));
            
            Assert.IsTrue(view.ConstrcutionContainer.Has<IConstructionView>());
            
            viewCollection.Dispose();
        }

        [TearDown]
        public void TestDisposables()
        {
            DisposeTests.TestDisposables();
        }

    }
}
