using System.Collections.Generic;
using Game.Assets.Scripts.Game.Logic.Infrastructure;
using Game.Assets.Scripts.Game.Logic.Models.Cutscenes.StepVariations;
using Game.Assets.Scripts.Game.Logic.Models.Sequencer;

namespace Game.Assets.Scripts.Game.Logic.Models.Cutscenes
{
    public class TimelineCutscene : Cutscene
    {
        public TimelineCutscene(LevelSequencer levelSequencer,  string name) : base(levelSequencer, 
            new List<CutsceneStep>() {new PlayTimelineAnimationStep(name)})
        {
            IInfrastructure.Default.Application.Music.Stop();
        }
    }
}