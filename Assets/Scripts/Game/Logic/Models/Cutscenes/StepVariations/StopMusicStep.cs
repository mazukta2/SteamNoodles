using System;
using Game.Assets.Scripts.Game.Environment.Engine;
using Game.Assets.Scripts.Game.Logic.Definitions.Cutscenes.Variations;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Infrastructure.Music;
using Game.Assets.Scripts.Game.Logic.Models.Time;
using Game.Assets.Scripts.Game.Logic.Views.Controls;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations
{
    public class StopMusicStep : CutsceneStep
    {
        private MusicManager _music;

        public StopMusicStep(StopMusic definition) : this(IInfrastructure.Default.Application.Music)
        {

        }

        public StopMusicStep(MusicManager music)
        {
            _music = music;
        }

        protected override void DisposeInner()
        {
        }

        public override void Play()
        {
            _music.Stop();

            FireOnFinished();
        }

    }
}

