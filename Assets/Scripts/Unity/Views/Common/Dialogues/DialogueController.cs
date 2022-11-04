using System;
using System.Collections.Generic;
using System.Linq;
using Game.Assets.Scripts.Game.Logic.Presenters.Localization;
using GameUnity.Assets.Scripts.Unity.Views.Ui.Common;
using GameUnity.Unity.Views.Common.Timeline;
using RedBlueGames.Tools.TextTyper;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace GameUnity.Unity.Views.Common.Dialogues
{
    public class DialogueController : MonoBehaviour
    {
        [SerializeField] UnityText _name;
        [SerializeField] TextTyper _text;
        [SerializeField] private Button _button;
        [SerializeField] private PlayableDirector _director;
        
        private Animator _animator;
        private bool _pause;
        private double _pauseTime;
        private Queue<DialogueControlAsset.Line> _lines;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _button.onClick.AddListener(HandleClick);
        }

        protected void OnDestroy()
        {
            _button.onClick.RemoveListener(HandleClick);
        }


        public void Pause()
        {
            _pauseTime = _director.time;
            _pause = true;
        }

        protected void Update()
        {
            if (_pause)
            {
                _director.time = _pauseTime;
            }
        }
        
        private void HandleClick()
        {
            if (!_pause)
                return;

            if (_lines.Count > 0)
            {
                TakeLine();
            }
            else
            {
                _pause = false;
                _animator.SetBool("Showing", false);
            }
        }

        public void ShowDialog(DialogueControlAsset.Line[] lines)
        {
            _lines = new Queue<DialogueControlAsset.Line>(lines);
            TakeLine();
        }

        private void TakeLine()
        {
            var line =_lines.Dequeue();
            ShowDialog(new LocalizatedString(line.Character), new LocalizatedString(line.Tag));
        }

        public void ShowDialog(ILocalizatedString characterName, ILocalizatedString text)
        {
            _animator.SetBool("Showing", true);
            _text.TypeText(text.Get(),0.001f);
            _name.Set(characterName.Get());
        }
    }
}