using UnityEngine;
using UnityEngine.Playables;

namespace GameUnity.Unity.Views.Common.Timeline
{
    public class DialogueControlAsset : PlayableAsset
    {
        [SerializeField] private string _character;
        [SerializeField] private string _tag;
        
        public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
        {
            var playable = ScriptPlayable<DialogueControlBehaviour>.Create(graph);

            var controlBehaviour = playable.GetBehaviour();
            controlBehaviour.SetText(_character, _tag);
            //lightControlBehaviour.light = light.Resolve(graph.GetResolver());
            //controlBehaviour.light = light.Resolve(graph.GetResolver());
            //controlBehaviour.color = color;
            //controlBehaviour.intensity = intensity;

            return playable;
        }
    }
}