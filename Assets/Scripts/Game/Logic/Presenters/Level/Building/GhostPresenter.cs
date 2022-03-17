using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Common.Math;
using Game.Assets.Scripts.Game.Logic.Definitions.Constructions;
using Game.Assets.Scripts.Game.Logic.Models.Building;
using Game.Assets.Scripts.Game.Logic.Models.Constructions;
using Game.Assets.Scripts.Game.Logic.Presenters.Level.Building.Placement;
using Game.Assets.Scripts.Game.Logic.Views.Level;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Constructions.Hand;
using Game.Assets.Scripts.Game.Logic.Views.Ui.Screens;
using System;
using static Game.Assets.Scripts.Game.Logic.Presenters.Ui.ScreenManagerPresenter;

namespace Game.Assets.Scripts.Game.Logic.Presenters.Ui.Constructions
{
    public class GhostPresenter : BasePresenter
    {
        public ConstructionDefinition Definition { get; private set; }
        
        private readonly GhostView _view;
        private readonly ConstructionsSettingsDefinition _constructionsSettings;
        private readonly IControls _controls;
        private readonly ConstructionCard _card;
        private readonly ConstructionsManager _constructionsManager;
        private readonly ScreenManagerPresenter _screenManager;
        private FloatPoint _worldPosition;

        public GhostPresenter(ConstructionsSettingsDefinition constructionsSettings, 
            ScreenManagerPresenter screenManager,
            ConstructionsManager constructionsManager,
            ConstructionCard card, IControls controls, GhostView view) : base(view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _constructionsSettings = constructionsSettings ?? throw new ArgumentNullException(nameof(constructionsSettings));
            _controls = controls ?? throw new ArgumentNullException(nameof(controls));
            _card = card ?? throw new ArgumentNullException(nameof(card));
            _constructionsManager = constructionsManager ?? throw new ArgumentNullException(nameof(constructionsManager));
            _screenManager = screenManager ?? throw new ArgumentNullException(nameof(screenManager));
            Definition = card.Definition ?? throw new ArgumentNullException(nameof(card.Definition));

            _controls.OnLevelClick += OnLevelClick;
        }

        protected override void DisposeInner()
        {
            _controls.OnLevelClick -= OnLevelClick;
        }

        public IntPoint GetPosition(PlacementField placementField)
        {
            var halfCell = _constructionsSettings.CellSize / 2;

            var offset = new FloatPoint(_constructionsSettings.CellSize * halfCell - halfCell,
                _constructionsSettings.CellSize * halfCell - halfCell);

            var pos = _worldPosition - offset;

            var mousePosX = Math.Round(pos.X / _constructionsSettings.CellSize);
            var mousePosY = Math.Round(pos.Y / _constructionsSettings.CellSize);

            return new IntPoint((int)Math.Ceiling(mousePosX), (int)Math.Ceiling(mousePosY));
        }

        private void OnLevelClick()
        {
            foreach (var field in _constructionsManager.Placements)
            {
                if (field.CanPlace(_card, GetPosition(field)))
                {
                    field.Build(_card, GetPosition(field));
                    break;
                }
            }
            _screenManager.GetCollection<CommonScreens>().Open<MainScreenView>();
        }

        public void MoveTo(FloatPoint worldPosition)
        {
            _worldPosition = worldPosition;
            //View.PlaceTo(_placement.GetWorldPosition(Position));
            //View.SetCanBePlacedState(CanPlaceGhost());
            //_placement.UpdateGhostCells();
        }

        //public bool CanPlaceGhost()
        //{
        //    return
        //}

        //protected override void DisposeInner()
        //{
        //    _model.OnAdded -= ScnemeAddedHandle;
        //}

        //private void ScnemeAddedHandle(ConstructionCard obj)
        //{
        //    var viewPresenter = _view.CardPrototype.Create<HandConstructionView>(_view.Cards);
        //    viewPresenter.Init(_screenManager, obj);
        //}



        //public ConstructionCard Card { get; }
        //public IGhostConstructionView View { get; private set; }
        //public Point Position { get; private set; }

        //private PlacementPresenter _placement;


        //public ConstructionGhostPresenter(PlacementPresenter placement, ConstructionCard originCard, IGhostConstructionView view)
        //{
        //    Card = originCard;
        //    _placement = placement;
        //    View = view;
        //    View.SetMoveAction(MoveTo);
        //    View.SetImage(Card.BuildingView);
        //}


        //protected override void DisposeInner()
        //{
        //    View.Dispose();
        //}



        //public void MoveTo(FloatPoint worldPosition)
        //{
        //    Position = GetCellPosition(worldPosition);
        //    View.PlaceTo(_placement.GetWorldPosition(Position));
        //    View.SetCanBePlacedState(CanPlaceGhost());
        //    _placement.UpdateGhostCells();
        //}
    }
}
