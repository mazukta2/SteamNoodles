using Assets.Scripts.Core;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.ViewModels.Buildings;
using Assets.Scripts.Views.Events;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : GameMonoBehaviour
    {
        public BuildingSchemeViewModel Scheme { get; private set; }

        private GameObject _ghost;
        private GameInputs _inputs = new GameInputs();
        private PlacementViewModel _placement;
        private HistoryReader _reader;

        public void Set(PlacementViewModel placement)
        {
            if (placement == null) throw new ArgumentNullException(nameof(placement));
            _placement = placement;
            _reader = new HistoryReader(_placement.History);
            _reader
                .Subscribe<PlacementViewModel.GhostSetEvent>(SetGhostHandle)
                .Subscribe<PlacementViewModel.GhostClearEvent>(ClearGhostHandle)
                .Update();
        }

        public void SetGhostHandle(PlacementViewModel.GhostSetEvent ev)
        {
            if (Scheme != null) throw new Exception("Clear ghost before changing it");

            Scheme = ev.BuildingScheme;
            _ghost = GameObject.Instantiate(Scheme.Ghost, transform);
        }

        public void ClearGhostHandle(PlacementViewModel.GhostClearEvent ev)
        {
            Scheme = null;
            GameObject.Destroy(_ghost);
            _ghost = null;
        }

        protected void Update()
        {
            _reader.Update();

            if (_ghost == null)
                return;

            _ghost.transform.position = _placement.GetGhostPosition();

            if (_inputs.IsTapedOnLevel())
            {
                if (_placement.CanPlace(_placement.Ghost, _placement.GetGhostCell()))
                {
                    _placement.BuildGhost();
                    _placement.ClearGhost();
                }
            }
        }
    }
}