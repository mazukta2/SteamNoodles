using System.Net.Mime;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using GameUnity.Unity.Views.Common.Dialogues;
using UnityEngine.Playables;

namespace GameUnity.Unity.Views.Common.Timeline
{
	public class DialogueControlBehaviour : PlayableBehaviour
	{
		private DialogueController _controller;
		bool _firstFrameHappened;
		bool _reachedFinal;
		private DialogueControlAsset.Line[] _lines;


		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			_controller = playerData as DialogueController;

			if (!_firstFrameHappened)
			{
				OnBehaviourPlay(playable, info);
				return;
			}

			//If we reached the final, check if we should pause the timeline until the player finish the talk
			if (playable.GetTime() >= playable.GetDuration() - (double)0.1)
			{
				if (!_reachedFinal)
				{
					_reachedFinal = true;
					_controller.Pause();
				}
			}
			else
			{
				_reachedFinal = false;
			}
		}

		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (!_controller)
				return;

			if (!_firstFrameHappened)
			{
				_firstFrameHappened = true;
				if (UnityEngine.Application.isPlaying)
					_controller.ShowDialog(_lines);
			}
		}

		public void SetText(DialogueControlAsset.Line[] lines)
		{
			_lines = lines;
		}
	}
}