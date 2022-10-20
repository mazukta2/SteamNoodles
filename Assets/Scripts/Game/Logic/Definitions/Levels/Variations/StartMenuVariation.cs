using System;
using Game.Assets.Scripts.Game.Logic.Common.Json.Convertors;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Models;
using Game.Assets.Scripts.Game.Logic.Models.Levels;
using Game.Assets.Scripts.Game.Logic.Models.Levels.Variations;
using Game.Assets.Scripts.Game.Logic.Models.Session;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Presenters;
using Newtonsoft.Json;

namespace Game.Assets.Scripts.Game.Logic.Definitions.Levels.Variations
{
    public class StartMenuVariation : LevelVariation
    {
        [JsonConverter(typeof(DefinitionsConventer<CutsceneDefinition>))]
        public CutsceneDefinition StartCutscene { get; set; }

        public override (ILevel, IPresenter) CreateModel(LevelDefinition definition, IModels models)
        {
            return (new StartMenu(this, models, IGameRandom.Default, IGameTime.Default, IDefinitions.Default), null);
        }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(SceneName))
                throw new Exception();

            if (StartCutscene == null)
                throw new System.Exception();
        }
    }
}
