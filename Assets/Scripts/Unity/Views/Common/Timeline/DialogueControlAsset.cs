using System;
using UnityEngine;
using UnityEngine.Playables;

namespace GameUnity.Unity.Views.Common.Timeline
{
    public class DialogueControlAsset : PlayableAsset
    {
        [SerializeField] private Line[] _lines;
        
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
        
        [Serializable]
        public struct Line
        {
            public string Character;
            public string Tag;
        }
    }
}