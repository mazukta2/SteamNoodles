using Assets.Scripts.Core;
using Assets.Scripts.Models.Buildings;
using Assets.Scripts.Views.Cameras;
using Assets.Scripts.Views.Events;
using System;
using UnityEngine;

namespace Assets.Scripts.Views.Buildings
{
    public class BuildingGhostView : GameMonoBehaviour
    {
        public BuildingScheme Scheme { get; private set; }

        private GameObject _ghost;
        private GameInputs _inputs = new GameInputs();
        private BuildingViewModel _buildings;
        private HistoryReader _reader;

        public void Set(BuildingViewModel buildings)
        {
            if (buildings == null) throw new ArgumentNullException(nameof(buildings));
            _buildings = buildings;
            _reader = new HistoryReader(_buildings.History);
            _reader
                .Subscribe<BuildingViewModel.GhostSetEvent>(SetGhost)
                .Subscribe<BuildingViewModel.GhostClearEvent>(ClearGhost)
                .Update();
        }

        public void SetGhost(BuildingViewModel.GhostSetEvent ev)
        {
            if (Scheme != null)
                throw new Exception("Clear ghost before changing it");

            Scheme = ev.BuildingScheme;
            _ghost = GameObject.Instantiate(Scheme.GetGhostPrefab(), transform);
        }

        public void ClearGhost(BuildingViewModel.GhostClearEvent ev)
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

            _ghost.transform.position = _buildings.GetGhostPosition();

            if (_inputs.IsTapedOnLevel())
            {
                var position = _buildings.GetGhostCell();
                if (_buildings.Ghost.CanBuild(_buildings.GetGrid(), position))
                {
                    _buildings.BuildGhost();
                    _buildings.ClearGhost();
                }
            }
        }
    }
}