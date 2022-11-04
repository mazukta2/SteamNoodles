using System.Net.Mime;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using GameUnity.Unity.Views.Common.Dialogues;
using UnityEngine.Playables;

namespace GameUnity.Unity.Views.Common.Timeline
{
	public class DialogueControlBehaviour : PlayableBehaviour
	{
		private string _tag;
		private DialogueController _controller;
		bool _firstFrameHappened;
		bool _reachedFinal;
		private string _characterName;


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
					_controller.ShowDialog(new LocalizatedString(_characterName), new LocalizatedString(_tag));
			}
		}

		public void SetText(string name, string tag)
		{
			_tag = tag;
			_characterName = name;
		}
	}
}