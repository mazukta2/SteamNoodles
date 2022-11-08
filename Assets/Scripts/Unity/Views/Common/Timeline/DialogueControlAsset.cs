using System;
using System.Windows.Forms.VisualStyles;
using Game.Assets.Scripts.Game.Logic.Definitions.Common;
using Game.Assets.Scripts.Game.Logic.Definitions.Languages;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using GameUnity.Assets.Scripts.Unity.Engine.Definitions;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Playables;

namespace GameUnity.Unity.Views.Common.Timeline
{
    public class DialogueControlAsset : PlayableAsset
    {
        [SerializeField] private Line[] _lines;
        
        [SerializeField] [ReadOnly]
        [ResizableTextArea]
        private string _text;
        
        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogueControlBehaviour>.Create(graph);

            var controlBehaviour = playable.GetBehaviour();
            controlBehaviour.SetText(_lines);
            //lightControlBehaviour.light = light.Resolve(graph.GetResolver());
            //controlBehaviour.light = light.Resolve(graph.GetResolver());
            //controlBehaviour.color = color;
            //controlBehaviour.intensity = intensity;

            return playable;
        }

        public void OnValidate()
        {
            var defs = new GameDefinitions(new UnityDefinitions());
            var localizationManager = new LocalizationManager(defs, "English");
            _text = "";
            for (var index = 0; index < _lines.Length; index++)
            {
                var line = _lines[index];
                var text = localizationManager.Get(line.Character) + ": '" + localizationManager.Get(line.Tag) + "'" +
                        System.Environment.NewLine;
                _text += text;
            }
        }
        
        
        [Serializable]
        public struct Line
        {
            public string Character;
            public string Tag;
        }
    }
}